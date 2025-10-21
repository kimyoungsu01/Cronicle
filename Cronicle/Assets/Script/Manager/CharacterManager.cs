using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public PlayerController playerController { get; private set; }
    private static CharacterManager _instance;
    public static CharacterManager instance 
    {
        get 
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        } 
    }

    public Player _player;
    public Player Player 
    { 
        get { return _player; }
        set { _player = value; }
    }

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
           if(_instance != this)
           {
               Destroy(gameObject);
           }
        }
    }
}
