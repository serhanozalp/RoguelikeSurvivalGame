public static class GameConstants
{
    public static class Player
    {
        public static class Movement
        {
            public const float MOVE_FORWARD_SPEED = 5f;
            public const float MOVE_BACKWARD_SPEED = 2f;
            public const float STRAFE_SPEED = 3f;
        }

        public static class Animation
        {
            public const float LOCOMOTION_BLENDTREE_LERPSCALE = 0.1f;
            public const int ARMS_LAYER_INDEX = 1;
            public const string ANIMATION_STATE_NAME_WEAPON_GRAB = "WeaponGrab";
            public const string ANIMATION_STATE_NAME_WEAPON_IDLE = "WeaponIdle";
            public const string ANIMATION_STATE_NAME_WEAPON_AIM = "WeaponAim";
            public const string ANIMATION_STATE_NAME_WEAPON_RELOAD = "WeaponReload";
            public const float ANIMATION_FADE_DURATION = 0.25f;
        }
    }
}