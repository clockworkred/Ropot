using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField]private float timeLimit;
    private float _timeConsume;
    private bool _isGameOver;

    private void Awake()
    {
        _isGameOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _timeConsume = timeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        _timeConsume -= Time.deltaTime;
        if (_timeConsume <= 0f)
        {
            _isGameOver = true;
        }
    }

    public bool GetGameState()
    {
        return _isGameOver;
    }
}
