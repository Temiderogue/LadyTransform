using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot :  Player
{
    private int randonNum;
    public override void LevelEnd()
    {
        GameC.Instance.LevelEnd(false);
    }

    public override void Start()
    {
        InputSystem.Instance.OnTouch += StartPlay;
        PlayAnim("Idle", true);
        for (int i = 0; i < _models.Length; i++)
        {
            _models[i].enabled = false;
        }

        switch (CurrentForm)
        {
            case Form.Dress:
                _models[0].enabled = true;
                break;
            case Form.Swimsuit:
                _models[1].enabled = true;
                break;
            case Form.Sportwear:
                _models[2].enabled = true;
                break;
            case Form.Farmer:
                _models[3].enabled = true;
                break;
            default:
                break;
        }
    }

    public override void Update()
    {
        if (GameC.Instance.isInGame)
        {
            if (CurrentForm != NeededForm && _isInZone)
            {
                _spineFollower.followSpeed = 2.5f;
            }
            else
            {
                _spineFollower.followSpeed = 5f;
            }
        }
        else
        {
            _spineFollower.followSpeed = 0f;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        PlayAnim("Walk", false);
        _isInZone = true;
        switch (other.tag)
        {
            case "Dress":
                PlayAnim("Walk", true);
                NeededForm = Form.Dress;
                break;
            case "Swimsuit":
                PlayAnim("Swim", true);
                //change animation to swim
                NeededForm = Form.Swimsuit;
                break;
            case "Sportwear":
                PlayAnim("Run", true);
                NeededForm = Form.Sportwear;
                break;
            case "Farmer":
                PlayAnim("Walk", true);
                NeededForm = Form.Farmer;
                break;
            case "LevelEnd":
                GameC.Instance.LevelEnd(true);
                break;
            default:
                break;
        }
        StartCoroutine(ChangeStance(other.tag));
    }

    public override void OnTriggerExit(Collider other)
    {
        PlayAnim("Run", false);
        PlayAnim("Walk", false);
        PlayAnim("Swim", false);
        PlayAnim("Walk", true);
        _isInZone = false;
    }

    private IEnumerator ChangeStance(string form)
    {
        randonNum = Random.Range(2,5);
        yield return new WaitForSeconds(randonNum);
        ChangeForm(form);
    }
}
