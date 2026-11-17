using System;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

namespace Gamekit3D.GameCommands
{
    public abstract class SimpleTransformer : GameCommandHandler
    {
        public enum LoopType
        {
            Once,
            PingPong,
            Repeat
        }

        public LoopType loopType;

        public float duration = 1;
        public AnimationCurve accelCurve;

        public bool activate = false;
        public SendGameCommand OnStartCommand, OnStopCommand;
        

        [Range(0, 1)]
        public float previewPosition;
        float time = 0f;
        float position = 0f;
        float direction = 1f;

        protected Platform m_Platform;

       //Declaring an EventInstance to use for playing and stopping doorSounds or similar types of SFX
        private EventInstance eventInstance;
        

        protected override void Awake()
        {
            base.Awake();

            m_Platform = GetComponentInChildren<Platform>();
        }

        
        //This is where the door starts opening
        public override void PerformInteraction()
        {
            activate = true;
            if (OnStartCommand != null) OnStartCommand.Send();
            Debug.Log("Door Opened");
        }

        public void FixedUpdate()
        {
            if (activate)
            {
                time = time + (direction * Time.deltaTime / duration);
                switch (loopType)
                {
                    case LoopType.Once:
                        LoopOnce();
                        break;
                    case LoopType.PingPong:
                        LoopPingPong();
                        break;
                    case LoopType.Repeat:
                        LoopRepeat();
                        break;
                }
                PerformTransform(position);
            }
        }

        public virtual void PerformTransform(float position)
        {
            
        }

        void LoopPingPong()
        {
            position = Mathf.PingPong(time, 1f);
        }

        void LoopRepeat()
        {
            position = Mathf.Repeat(time, 1f);
        }

       //This is where the door closes
        void LoopOnce()
        {
            position = Mathf.Clamp01(time);
            if (position >= 1)
            {
                enabled = false;
                if (OnStopCommand != null) OnStopCommand.Send();
                direction *= -1;
                Debug.Log("Door Closed");
            }
        }
    }
}
