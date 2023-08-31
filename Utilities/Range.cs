namespace Utilities
{
    [System.Serializable]
    public struct Range
    {
        public float min;
        public float max;
        public float value;

        public Range(float min, float max)
        {
            this.min = min;
            this.max = max;
            this.value = min;
        }
    }
}