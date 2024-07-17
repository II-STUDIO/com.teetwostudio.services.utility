namespace Services
{
    public interface ILoopUpdateEntity
    {
        public bool IsUpdatable { get; }
        /// <summary>
        /// This called every frame on main thrend.
        /// </summary>
        /// <param name="deltaTime">Time between this frame and previouse frame</param>
        void LoopUpdateEvent(float deltaTime);
    }
}