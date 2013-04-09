namespace Kids.Utility.Ping_Helper
{
    public class Validation
    {
        private Validation()
        {
        }

        public static bool IsFlagged(int flaggedEnum, int flaggedValue)
        {
            if ((flaggedEnum & flaggedValue) != 0)
                return true;

            return false;
        }
    }
}