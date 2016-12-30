using ExampleNetwork.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExampleNetwork.Controller
{
    class ControllerPlayer
    {
        private static ControllerPlayer instance;
        public static ControllerPlayer Instance
        {
            get
            {
                if (instance == null) instance = new ControllerPlayer();
                return instance;
            }
        }

        private ControllerPlayer()
        {
            state = PlayerState.STAND;
            direction = Direction.DOWN;
        }

        private PlayerState state;
        private Direction direction;

        public PlayerState State { get { return state; } }
        public Direction Direction { get { return direction; } }

        internal void setState(MessageControl? control)
        {
            if (control == null)
                return;

            switch (control.Value)
            {
                case MessageControl.STOP:
                    state = PlayerState.STAND;
                    break;
                case MessageControl.MOVE_LEFT:
                    state = PlayerState.MOVE;
                    direction = Direction.LEFT;
                    break;
                case MessageControl.MOVE_UP:
                    state = PlayerState.MOVE;
                    direction = Direction.UP;
                    break;
                case MessageControl.MOVE_RIGHT:
                    state = PlayerState.MOVE;
                    direction = Direction.RIGHT;
                    break;
                case MessageControl.MOVE_DOWN:
                    state = PlayerState.MOVE;
                    direction = Direction.DOWN;
                    break;
            }
        }

        internal bool IsStand()
        {
            return state == PlayerState.STAND;
        }
    }
}
