using UnityEngine;
using UnityEngine.Serialization;

public class PlayerFlyScript : MonoBehaviour {

	public static PlayerFlyScript Instance;
	private Rigidbody2D _rb;

	[FormerlySerializedAs("forceX")] [SerializeField]
	private float velocityX;

	[SerializeField]
	private float forceY;

	[SerializeField]
	private bool _didMove;


	void  Awake()
	{
		MakeInstance();
	}
	

	void Update(){
		Initialize ();
		_rb.mass = Settings.Instance.mass;
	}

	void Initialize(){
		_rb = GetComponent<Rigidbody2D> ();
	}

	public void MakeInstance(){
		if (Instance == null)
			Instance = this;
	}
	
	public void Fly(double loudness){
		
		if (_rb)
		{
			forceY = (float) (Settings.Instance.forceYMultiplier * loudness);
			if (loudness > Settings.Instance.walkThreshold)
			{
				velocityX = (float)Settings.Instance.velocityX;
			}
			else
			{
				velocityX = 0;
			}
			_rb.velocity = new Vector2(velocityX, 0);
			_rb.AddForce(new Vector2(0,forceY));

			_didMove = true;
		}
	}
	void OnTriggerEnter2D(Collider2D target){
		if (_didMove)
		{
			_didMove = false;
			if (target.tag == "Finish")
			{
				//tODO saveScore and level
				FinishGame(true);
			}
		}

		if (target.tag == "dead" || target.tag == "Enemy")
		{
			FinishGame(false);
		}

	}

	private void FinishGame(bool win)
	{
		if (GameOverManager.instance != null) {
			GameOverManager.instance.showGameOverPanel (win);
		}
		if(!win)
			Destroy (gameObject);
		else
		{
			enabled = false;
		}
	}
	
}
