namespace AlgorithmsIlluminated_I
{
    internal class Program
    {
        public static void Main()
        {
            Console.WriteLine(Sub("45", "25"));

            int[] ToInt(char[] charArray)
            {
                var res = new int[charArray.Length];
                for (int i = 0; i < charArray.Length; i++)
                    res[i] = charArray[i] - '0';
                return res;
            }

            (string xAligned, string yAligned) Align(string x, string y)
            {
                var n = Math.Max(x.Length, y.Length);
                return (x.PadLeft(n, '0'), y.PadLeft(n, '0'));
            }

            string Add(string x, string y)
            {
                var (xAligned, yAligned) = Align(x, y);

                int[] xDigits = ToInt(xAligned.ToCharArray());
                int[] yDigits = ToInt(yAligned.ToCharArray());
                int[] sum = new int[xDigits.Length + 1];
                int carr = 0;

                for (int i = sum.Length - 1; i > 0; i--)
                {
                    var ab = xDigits[i - 1] + yDigits[i - 1] + carr;
                    sum[i] = ab % 10;
                    carr = ab / 10;
                }

                sum[0] = carr;
                var result = "";
                for (int i = sum[0] == 0 ? 1 : 0; i < sum.Length; i++)
                    result += sum[i];

                return result;
            }

            string Sub(string x, string y)
            {
                var (xAligned, yAligned) = Align(x, y);

                int[] xDigits = ToInt(xAligned.ToCharArray());
                int[] yDigits = ToInt(yAligned.ToCharArray());
                int[] sum = new int[xDigits.Length];
                int carr = 0;

                for (int i = sum.Length - 1; i >= 0; i--)
                {
                    var ab = xDigits[i] - yDigits[i] - carr;
                    sum[i] = Math.Abs(ab % 10);
                    carr = Math.Abs(ab / 10);
                }

                var result = "";
                var startIndex = 0;
                for (int i = 0; i < sum.Length; i++)
                    if (sum[0] == 0)
                        startIndex++;
                    else
                        break;
                for (int i = startIndex; i < sum.Length; i++)
                    result += sum[i];

                return result;
            }
        }
    }
}