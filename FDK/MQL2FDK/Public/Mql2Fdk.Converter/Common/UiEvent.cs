namespace Mql2Fdk.Converter.Common
{
    public struct UiEvent
    {
        public delegate void OnUiEvent(object data);

        public OnUiEvent OnEvent;

        public void Notify(object data = null)
        {
            if (OnEvent != null)
                OnEvent.Invoke(data);
        }
    }
}