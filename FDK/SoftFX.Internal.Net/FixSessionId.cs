namespace SoftFX.Internal
{
    public class FixSessionId
    {
        public string BeginString { get; set; }
        public string SenderCompId { get; set; }
        public string TargetCompId { get; set; }

        public override string ToString()
        {
            var result = string.Format("{0}:{1}->{2}", this.BeginString, this.SenderCompId, this.TargetCompId);
            return result;
        }
    }
}
