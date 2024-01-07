namespace Services
{
    public interface ITransitionHandle
    {
        public void OnFadeInBegin();
        public void OnFadeInComplete();
        public void OnFadeOutBegin();
        public void OnFadeOutComplete();
    }
}