
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour {

    ///////////////////////// Configuracion oleadas //////////////////////////////////////////////////////////////////////

    public float waveTime;          //Tiempo entre una oleada y la siguiente
    public float waveDelay;         //Tiempo de espera al iniciar el piso
    public float bossTime;          //Tiempo entre que se spawnea el boss y pierdes.
    public float maxTime;           //Tiempo total (suma de los anteriores)
    public GameObject[] waves;      //objeto que contiene las oleadas. se activan para que la oleada aparezca


    //////////////////////////////  UI//////////////////////////////////////////////////////////////////////////

    public Text startDelayText;
    public Text maxTimeText;
    public Text waveTimeText;
    public Slider waveSlider;

    ///////////////////////////////// Debug //////////////////////////////////////////////////////////////////////////////

    public float actualTime;       //Cuenta regresiva del tiempo total
    public float actualWaveTime;   //tiempo restante para la siguiente oleada
    public float actualDelay;      //Sirve para iniciar la primera oleada 
    public int nextWave;          //almacena la siguiente oleada que debe spawnearse 
    public bool start;
    public int totalWaves;
    public bool boss = false;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	void Start () {
        totalWaves = waves.Length;
        maxTime =  waveTime * totalWaves + bossTime;
        actualWaveTime = 0;
        actualTime = maxTime;
        actualDelay = waveDelay;
        nextWave = 0;
        start = false;

        startDelayText.text = actualDelay.ToString();
        maxTimeText.text = actualTime.ToString();
        waveTimeText.text = waveTime.ToString();
        waveSlider.maxValue = maxTime;
        waveSlider.value = maxTime;


	}

	
	void Update () {
        if (start)
        {
            actualTime -= Time.deltaTime;

            if (actualWaveTime <= 0)
            {
                if (nextWave <= totalWaves - 1)
                {
                    ///aun se pueden spawnear oleadas

                    waves[nextWave].gameObject.SetActive(true);

                    nextWave++;
                    actualWaveTime = waveTime;
                }
                else
                {
                    //se alcanzo la ultima oleada. toca spawnear al boss
                    if (!boss)
                    {
                        boss = true;
                        //invocar boss
                        actualWaveTime = bossTime;
                    }
                }


            }
            else
            {
                actualWaveTime -= Time.deltaTime;
            }

            waveTimeText.text = Mathf.Round(actualWaveTime) + "";
            maxTimeText.text = Mathf.Round(actualTime) + "";
            waveSlider.value = actualTime;

        }
        else
        {
            actualDelay -= Time.deltaTime;
            startDelayText.text = Mathf.Round(actualDelay) + "";
            if (actualDelay <= 0)
            {
                start = true;
                startDelayText.enabled = false;
            }
        }
	}
}
