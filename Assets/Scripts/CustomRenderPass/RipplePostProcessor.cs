using UnityEngine;
using Zenject;

public class RipplePostProcessor : IInitializable, ITickable
{
    [System.Serializable]
    public class RippleSettings
    {
        [field: SerializeField] public Material RippleMaterial { get; private set; }
        [field: SerializeField] public float MaxAmount { get; private set; } = 50;
        [field: SerializeField] public float Friction { get; private set; } = 0.9f;
        
        public const float LOWEST_AMOUNT_VALUE = 0.0001f;
        public const float EXPECTED_FRAME_TIME = 1 / 60f;
    }

    private RippleSettings _settings;

    // i think injecting to URP render execution is not worth it. thats why its static
    // player will always play on one screen only, unless we'll implement couch co-op
    public static float CurrentAmount => _currentAmount;
    private static float _currentAmount;
    private bool _requestInDemand;
    
    private bool _update = false;

    [Inject]
    private void Construct(RippleSettings set)
    {
        _settings = set;
    }

    public void RequestRipple()
    {
        _requestInDemand = true;
    }

    public void Initialize()
    {
        _currentAmount = _settings.RippleMaterial.GetFloat("_Amount");
        _update = _currentAmount > RippleSettings.LOWEST_AMOUNT_VALUE;
    }
    
    public void Tick()
    {
        if (_requestInDemand)
        {
            _requestInDemand = false;
            ApplyRipple();
        }
        
        if (_update)
        {
            _settings.RippleMaterial.SetFloat("_Amount", _currentAmount);
            float amountToReduce = _currentAmount * (1 - _settings.Friction);
            _currentAmount -= amountToReduce * (Time.deltaTime / RippleSettings.EXPECTED_FRAME_TIME);
            
            if (_currentAmount < RippleSettings.LOWEST_AMOUNT_VALUE)
            {
                _update = false;
                _currentAmount = 0;
                _settings.RippleMaterial.SetFloat("_Amount", _currentAmount);
            }
        }
    }
    
    private void ApplyRipple()
    {
        _currentAmount = _settings.MaxAmount;
        Vector2 pos = new Vector2(Screen.width, Screen.height) / 2f;
        _settings.RippleMaterial.SetFloat("_CenterX", pos.x);
        _settings.RippleMaterial.SetFloat("_CenterY", pos.y);
        _update = true;
    }
}