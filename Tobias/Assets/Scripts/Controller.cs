using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
 
public class Controller : MonoBehaviour {
	public float speed;
	private Rigidbody2D rg2d;
	private Vector2 move;
	public AudioSource flying;
	public AudioSource eating;
	public AudioSource exploding;
	public static bool estate;
	private Animator animator;
	public Image[] gor;
	public static int feed;
	public static Transform posicao;
	public Text pontos;
	private int lastperc;
	private int gordura;
	private int eatcount;
	public static bool vida;
	public BoxCollider2D boxcollider;
	public BoxCollider2D boxcollider1;
    public BoxCollider2D boxcollider2;
	public RuntimeAnimatorController[] controllers;
	private float horizontal;
	private float vertical;
	public Text fps;
    private float deltaTime = 0.0f;
    public CNJoystick joystick;
	public delegate void Death();
	public static event Death OnDeath;
	private bool voandoprasempre=false;


    

	void Awake () {
		for (int i=1; i <gor.Length; i++) {
			gor[i].enabled=false;
		}
		posicao = GetComponent<Transform> ();
		eatcount=0;
		lastperc = 0;
		gordura = 0;
		feed = 0;
		pontos.text = feed.ToString();
		rg2d = GetComponent<Rigidbody2D> ();
		estate = false;
		animator = GetComponent<Animator> ();
		vida = true;

        
		
	}
	public static void ShowAd(){
		if (Advertisement.IsReady("video"))
		{
			Advertisement.Show();

		}

	}


	public void DevorarTobias(){
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();
		renderer.enabled = false;
		tag = "Dead";
	
	}
	void OnTriggerExit2D(Collider2D other) {

		if (other.CompareTag ("LineofDeath")) {

			other.GetComponent<LineRenderer>().SetColors(new Color32(0,255,0,255),new Color32(0,255,0,255));
		}
	}

	

	void OnTriggerEnter2D(Collider2D other) {
		if (vida) {
			if (other.CompareTag ("Food")) {
				if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Tag2")){
					Vector3 inverso = transform.localScale;
					inverso.x*=-1;
					transform.localScale=inverso;
				}
                

				animator.SetTrigger("comer");
				eating.Play ();
				Destroy (other.gameObject);
				feed++;
				pontos.text = feed.ToString ();
				if (gordura > 0) {
					eatcount++;
					if (eatcount == 5) {
						eatcount = 0;
						gordura--;
						StartCoroutine(ComerGordo());
						speed += 0.22f;
						rg2d.mass -= 0.2f;
						gor [lastperc].enabled = false;
						lastperc--;
						gor [lastperc].enabled = true;
						
					}
				}
			} else if (other.CompareTag ("Badfood")) {
				if(animator.GetCurrentAnimatorStateInfo(0).IsTag("Tag2")){
					Vector3 inverso = transform.localScale;
					inverso.x*=-1;
					transform.localScale=inverso;
				}
                
				animator.SetTrigger("comer");
				eating.Play ();
				Destroy (other.gameObject);
				eatcount = 0;

				if (rg2d.mass < 0.8) {
                    speed -= 0.22f;
					rg2d.mass += 0.2f;
					gor [lastperc].enabled = false;
					lastperc++;
					gor [lastperc].enabled = true;
					gordura++;
					StartCoroutine(ComerGordo());





				} else if (rg2d.mass >= 0.8) {

				Explodir();
					

				}




			} else if (other.CompareTag ("Obstaculo")) {
				Explodir();
		
			}
			else if (other.CompareTag("DeadEnemy")){
				Explodir();
			}

			else if (other.CompareTag("LineofDeath")){

				other.GetComponent<LineRenderer>().SetColors(new Color32(255,0,0,255),new Color32(255,0,0,255));
			}
		}
	}
		




	

	void Update (){


	 if (vida) {
		
            
			if (Input.GetKeyUp (KeyCode.Escape)) {
				Explodir();


			}

            horizontal = joystick.GetAxis("Horizontal");
            vertical = joystick.GetAxis("Vertical");
            move.x = horizontal;
            move.y = vertical;
			bool voando = (horizontal!=0 || vertical!=0);
			if(!voandoprasempre){
				Voar(voando);
			}
			ChangePitch (voando);
			ChangeDirection ();
			transform.Translate(move * speed * Time.deltaTime);
        
        }

		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fpsf = 1/deltaTime;
        fpsf=Mathf.FloorToInt(fpsf);
        fps.text ="FPS: "+ fpsf.ToString();
	
}
	void Voar(bool voar){
		if (voar) {
			voandoprasempre = true;
			animator.SetBool ("Voando", voandoprasempre);
		}
	}

	void ChangeDirection()
	{
		if (move.x>0f) {
			Vector3 inverso = transform.localScale;
			inverso.x = -0.2f;
			transform.localScale = inverso;

		} 
		else if(move.x<0f)
		{
			Vector3 inverso = transform.localScale;
			inverso.x = 0.2f;
			transform.localScale = inverso;

		}
	}

	void ChangePitch(bool voando){
		if (voando && !flying.isPlaying) {
			flying.Play();
		}
		if (voando) {
			flying.pitch = 1.3f;
			animator.SetFloat("VelocidadeVoo", 2.5f);
		} 
		else {
			flying.pitch = 1.1f;
			animator.SetFloat("VelocidadeVoo", 1);
		}
	
	}
  
   

	void Explodir(){
        move = new Vector2(0, 0);
		StartCoroutine (GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManager> ().GameOver ());
		vida = false;
		estate = false;
		joystick.enabled = false;
		joystick.gameObject.SetActive (false);
		if (OnDeath != null) {
			OnDeath ();
		}

		animator.SetTrigger("explode");

		CircleCollider2D circleCollider = GetComponent<CircleCollider2D> ();
		circleCollider.enabled = true;
		boxcollider.enabled = false;
		boxcollider1.enabled = false;
        boxcollider2.enabled = false;
		Edge.bottomCollider.GetComponent<BoxCollider2D> ().enabled = false; 
		rg2d.freezeRotation = false;
		flying.Stop ();
		exploding.Play ();
		rg2d.mass = 0.15f;
		rg2d.drag = 0;
		rg2d.angularDrag = 0;
		rg2d.gravityScale = 0.5f;
		float random;
		if (transform.position.x <= 0) {
			random = -1f;
		} 
		else {
			random= 1f;
		}
		rg2d.AddForce(new Vector2(-100f*random,170f));




	}

	void UpdateSprites (){
		animator.runtimeAnimatorController = controllers [gordura];

	}


	IEnumerator ComerGordo(){

		yield return new WaitForSeconds (0.225f);

		UpdateSprites ();
		animator.SetBool ("Voando", voandoprasempre);
	}






}
