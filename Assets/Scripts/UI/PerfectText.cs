using System;
using System.Collections;
using TFPlay.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PerfectText : MonoBehaviour
{

    //Perfect run show text
    [SerializeField] private TextMeshProUGUI[] perfectText;
    [SerializeField] private ParticleSystem[] _emojis;

    private TextMeshProUGUI showText;
    //[SerializeField] private ParticleSystem _emojiShow;

    private int textIndex;
    private int tempTextIndex;

    private int emojiIndex;
    private int tempEmojiIndex;

    private int _randomNum;

    private void Start()
    {
        GameC.Instance.OnTiming += CoolTimingText;
    }

    private void CoolTimingText()
    {

        _emojis[UnityEngine.Random.Range(0, _emojis.Length)].Play();

        //if (showText != null)
        //{
        //    showText.gameObject.SetActive(false);
        //}

        
        //_randomNum = UnityEngine.Random.Range(0, 100);
        //Debug.Log(_randomNum);
        //if (_randomNum < 50)
        //{
        //    StopAllCoroutines();
        //    StartCoroutine(CoolTextShow());
        //}
        //else
        //{
        //    if (_emojiShow != null)
        //    {
        //        _emojiShow.Stop();
                
        //    }
        //    EmojiShow();
        //}
    }

    //private void EmojiShow()
    //{
    //    do
    //    {
    //        emojiIndex = UnityEngine.Random.Range(0, _emojis.Length);
    //    }
    //    while (emojiIndex == tempEmojiIndex);
    //    tempEmojiIndex = emojiIndex;
    //    _emojiShow = _emojis[emojiIndex];

    //    _emojiShow.Play();
    //}

    private IEnumerator CoolTextShow()
    {
        do
        {
            textIndex = UnityEngine.Random.Range(0, perfectText.Length);
        }
        while (textIndex == tempTextIndex);
        tempTextIndex = textIndex;
        showText = perfectText[textIndex];


        showText.transform.DOScale(new Vector3(1, 1, 1), 0);
        showText.gameObject.SetActive(true);

        showText.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.3f);
        yield return new WaitForSeconds(2f);
        showText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        GameC.Instance.OnTiming -= CoolTimingText;
    }
}