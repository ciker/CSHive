namespace System
{
    /// <summary>
    /// 算术扩展
    /// </summary>
    public static class MathExtension
    {
        /// <summary>
        /// 将某一数尽量平均分配到一个数组中去
        /// <remarks>
        /// 
        /// </remarks>
        /// </summary>
        /// <param name="totalAmount"></param>
        /// <param name="arrLength">注意，数组太长会抛异常的</param>
        /// <returns></returns>
        public static int[] AverageToArray(this int totalAmount, int arrLength)
        {
            try
            {
                var couVal = totalAmount;
                var arr = new int[arrLength];
                var val = totalAmount / arrLength;
                //Console.WriteLine($"val:{val}");
                if (val != 0)
                {
                    for (int i = 0; i < arrLength; i++)
                    {
                        arr[i] = val;
                        couVal -= val;
                    }
                }

                var yu = totalAmount % arrLength;
                //Console.WriteLine($"yu:{yu}");
                var addVal = yu > 0 ? 1 : -1;
                if (yu != 0)
                {
                    for (int i = 0; i < arrLength; i++)
                    {
                        if (couVal == 0) break;
                        arr[i] = arr[i] + addVal;
                        couVal -= addVal;
                    }
                }
                return arr;
            }
            catch (Exception)
            {
                throw new ArgumentException($"{arrLength} is too long.");
            }
           
        }
    }
}