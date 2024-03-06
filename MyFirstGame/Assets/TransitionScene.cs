using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    private static TransitionScene instance;
    public static TransitionScene Instance{
        get{return instance;}
        set{}
    }

    [SerializeField] Animator anim;

    void Awake(){
        if(instance == null){
            instance = this;
        } else {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void TransScene(string name){
        StartCoroutine(StartAnim(name));
    }

    IEnumerator StartAnim(string name){
        anim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(name);
        anim.SetTrigger("Start");
    }
}
