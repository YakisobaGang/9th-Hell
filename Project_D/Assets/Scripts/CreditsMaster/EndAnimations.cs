using System.Collections;
using UnityEngine;

namespace ProjectD.CreditsMaster
{
    public class EndAnimations : MonoBehaviour
    {
        public int startTime;
        public Animator anim2, anim3;
        public Animator animF, animL, animS, animT;
        public GameObject parteUm;
        public bool secondPart, fabricioPart, silvanoPart, luanaPart, thiagoPart, thirdPart = false;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SecondPart(startTime));
        }

        // Update is called once per frame
        void Update()
        {
            if (secondPart == true)
            {
                StartCoroutine(Fabricio(1));
                secondPart = false;
            }
            if (luanaPart == true)
            {
                StartCoroutine(Luana(1));
                luanaPart = false;
            }
            if (silvanoPart == true)
            {
                StartCoroutine(Silvano(1));
                silvanoPart = false;
            }
            if (thiagoPart == true)
            {
                StartCoroutine(Thiago(1));
                thiagoPart = false;
            }
            if (thirdPart == true)
            {
                StartCoroutine(ThirdPart(1));
            }
        }

        IEnumerator SecondPart(int t)
        {
            yield return new WaitForSeconds(t);
            anim2.SetTrigger("StartSecond");
            parteUm.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            secondPart = true;
        }

        IEnumerator Fabricio(int t)
        {
            animF.SetTrigger("FabricioStart");
            yield return new WaitForSeconds(4);
            luanaPart = true;
        }

        IEnumerator Luana(int t)
        {
            animL.SetTrigger("LuanaStart");
            yield return new WaitForSeconds(3);
            silvanoPart = true;
        }

        IEnumerator Silvano(int t)
        {
            animS.SetTrigger("SilvanoStart");
            yield return new WaitForSeconds(4);
            thiagoPart = true;
        }

        IEnumerator Thiago(int t)
        {
            animT.SetTrigger("ThiagoStart");
            yield return new WaitForSeconds(3);
            anim2.SetTrigger("EndSecond");
            yield return new WaitForSeconds(1);
            thirdPart = true;
        }

        IEnumerator ThirdPart(int t)
        {
            anim3.SetTrigger("ThirdStart");
            yield return new WaitForSeconds(0);
        }
    }
}
