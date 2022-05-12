using Dreamteck.Splines;
using TFPlay.UI;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public SplineFollower _spineFollower;
    public SkinnedMeshRenderer[] _models;
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Transform _levelEnd;
    [SerializeField] private Text _status;
    [SerializeField] private Animator[] _animators;
    public Form NeededForm = Form.Dress;
    public Form CurrentForm;
    public bool _isInZone;
    private float _fullDist;

    public virtual void Start()
    {
        InputSystem.Instance.OnTouch += StartPlay;
        //PlayAnim("Idle", true);
        _status.gameObject.SetActive(false);
        _fullDist = _levelEnd.position.z;
        for (int i = 0; i < _models.Length; i++)
        {
            _models[i].enabled =false;
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

    public virtual void Update()
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
        
        _progressBar.SetProgress(transform.position.z / _fullDist);
    }
    
    public enum Form
    {
        Dress,
        Swimsuit,
        Sportwear,
        Farmer
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        PlayAnim("Walk", false);
        Debug.Log("enter");
        _isInZone = true;
        _status.gameObject.SetActive(true);
        _status.text = other.tag;

        switch (other.tag)
        {
            case "Dress":
                PlayAnim("Walk",true);
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

        if (CurrentForm == NeededForm)
        {
            GameC.Instance.CoolTiming();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        PlayAnim("Run", false);
        PlayAnim("Walk", false);
        PlayAnim("Swim", false);
        PlayAnim("Walk", true);
        _isInZone = false;
        _status.gameObject.SetActive(false);
    }

    public virtual void LevelEnd()
    {
        GameC.Instance.LevelEnd(true);
    }

    public void ChangeForm(string form)
    {
        if (CurrentForm != (Form)System.Enum.Parse(typeof(Form), form))
        {
            for (int i = 0; i < _models.Length; i++)
            {
                _models[i].enabled = false;
            }
            switch (form)
            {
                case "Dress":
                    CurrentForm = Form.Dress;
                    _models[0].enabled = true;
                    break;
                case "Swimsuit":
                    CurrentForm = Form.Swimsuit;
                    _models[1].enabled = true;
                    break;
                case "Sportwear":
                    CurrentForm = Form.Sportwear;
                    _models[2].enabled = true;
                    break;
                case "Farmer":
                    CurrentForm = Form.Farmer;
                    _models[3].enabled = true;
                    break;
                default:
                    break;
            }

            if (_isInZone)
            {
                switch (NeededForm)
                {
                    case Form.Dress:
                        PlayAnim("Walk", true);
                        break;
                    case Form.Swimsuit:
                        PlayAnim("Swim", true);
                        break;
                    case Form.Sportwear:
                        PlayAnim("Run", true);
                        break;
                    case Form.Farmer:
                        PlayAnim("Walk", true);
                        break;
                    default:
                        break;
                }
            }

        }

        
    }

    public void PlayAnim(string animName,bool play)
    {
        for (int i = 0; i < _animators.Length; i++)
        {
            _animators[i].SetBool(animName, play);
        }
    }

    public void StartPlay()
    {
        PlayAnim("Idle", false);
        PlayAnim("Walk", true);
    }

    private void OnDestroy()
    {
        InputSystem.Instance.OnTouch -= StartPlay;
    }
}
