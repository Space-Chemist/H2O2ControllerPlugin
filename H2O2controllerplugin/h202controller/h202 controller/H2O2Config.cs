using ProtoBuf;
using Torch;

namespace h202_controller
{
    public class H2O2Config : ViewModel
    {
        [ProtoMember(1)]

        private bool _enabled = true;
        private float _production = 1.0f;
        private float _power = 1.0f;

        public bool Enabled
        {
            get => _enabled; 
            set => SetValue(ref _enabled, value);
        }
        
        public float Production
        {
            get => _production;
            set => SetValue(ref _production, value);
        }
        
        public float Power
        {
            get => _power;
            set => SetValue(ref _power, value);
        }
    }
}