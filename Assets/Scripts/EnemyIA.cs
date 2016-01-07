using UnityEngine;
using System.Collections;

public class EnemyIA : MonoBehaviour {

    private Vector3 targetposition;
    private Vector3 deltaPosition;
    public float speed;
    private float x;
    private Animator animator;
	public int CountComida=0;
	public BoxCollider2D radius;
	public bool chupar;
	private bool parar;
	public GameObject filho;
	private float InicioChupo;
	private float TerminarChupo;
	private IEnumerator coroutinedeath;
	private bool vivo;
	public LineRenderer LineofDeath;
	private Vector3 posicaoenemy;
	private Vector3 posicaoplayeratirar;
	private PolygonCollider2D ColliderLine;
	private Vector3 temp;
	public int ID;


	void Awake () {

        if (gameObject.CompareTag("EnemyLeft"))
        {
            x = transform.position.x + 5.5f;
        }
        else {
            x = transform.position.x - 5.5f;
        }

        
        targetposition.Set(x, -4.21f, 0.5f);
        animator = GetComponent<Animator>();
		chupar = false;
		parar = true;
		vivo = true; 
		coroutinedeath = Death ();
		TerminarChupo = 1.8f;

	}

	void OnTriggerEnter2D(Collider2D other) {

		if(vivo && !parar){
			if (other.CompareTag ("Badfood")|| other.CompareTag("ChupaFood")) {
				Destroy (other.gameObject);
				animator.SetTrigger ("comer");

			} else if (other.CompareTag ("Food")|| other.CompareTag("ChupaFood")) {
				Destroy (other.gameObject);
				animator.SetTrigger ("comer");
				CountComida++;
				if (CountComida == 3 && !chupar) {
					SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
					renderer.color = new Color (0.9f, 0.4f, 0.4f);
					filho.SetActive (true);
					GetComponentInParent<EnemyControl>().ZerarContador();
					chupar = true;
					InicioChupo = Time.time;
					TerminarChupo += InicioChupo;

				
				}
		
			} else if(other.CompareTag("Player")){
				other.gameObject.GetComponent<Controller>().DevorarTobias();
				animator.SetTrigger("comer");

			}

		}
	}
	void Explodir(){
		tag = "DeadEnemy";
		speed = 20f;
		animator.SetTrigger("explode");
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.color = Color.white;
		posicaoenemy = posicaoplayeratirar - transform.position;
		posicaoenemy.z = 0;



		
}
	void CheckVoo(){
		if (transform.position.x != deltaPosition.x) {
			
			animator.SetBool("Voando",true);
		} else {
			animator.SetBool ("Voando",false);
			parar = false;
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (vivo) {
			if (Controller.estate && parar) {
				deltaPosition = transform.position;
				transform.position = Vector3.MoveTowards (transform.position, targetposition, speed * Time.deltaTime);
				CheckVoo();
            
			}

			if (chupar && Time.time >= TerminarChupo) {

				chupar = false;
				filho.SetActive (false);
				StartCoroutine(coroutinedeath);

			}
		} else {
			transform.Translate(posicaoenemy.normalized*Time.deltaTime*speed);
		}



    
	
	}
	void AddColliderLine(){
		ColliderLine=LineofDeath.gameObject.AddComponent<PolygonCollider2D> ();
		ColliderLine.isTrigger = true;
		Vector2[] posicoes;
		posicoes = new Vector2[5];
		posicoes [0] = new Vector2 (transform.position.x, transform.position.y);
		posicoes [1] = new Vector2 (temp.x+0.1f, temp.y+0.1f);
		posicoes [4] = new Vector2 (temp.x-0.1f, temp.y-0.1f);
		posicoes [2] = new Vector2 (transform.position.x+0.1f, transform.position.y+0.1f);
		posicoes [3] = new Vector2 (transform.position.x-0.1f, transform.position.y-0.1f);
		ColliderLine.SetPath (0, posicoes);
	}


	IEnumerator Death(){

		posicaoplayeratirar = Controller.posicao.transform.position;
		LineofDeath.SetPosition(0 , new Vector3(transform.position.x,transform.position.y,0.7f));
		temp = Vector3.LerpUnclamped(transform.position,Controller.posicao.transform.position,5f);
		temp.z = 0.7f;
		LineofDeath.SetPosition(1,new Vector3(temp.x,temp.y,temp.z));
		LineofDeath.enabled=true;
		AddColliderLine ();
		vivo = false;
		yield return new WaitForSeconds (1);
		Explodir ();

		yield return new WaitForSeconds (1);
		Destroy (ColliderLine);
		LineofDeath.enabled=false;

		yield return new WaitForSeconds (0.5f);
		GetComponentInParent<EnemyControl> ().InstantiateEnemy (ID,LineofDeath);
		Destroy (gameObject);

	}
}
