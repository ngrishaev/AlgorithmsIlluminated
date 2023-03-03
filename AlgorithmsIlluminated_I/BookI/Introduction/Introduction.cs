namespace AlgorithmsIlluminated_I.BookI.Introduction;

public static class Introduction
{
    public static int RecIntMult(int x, int y)
    {
        int n = GetLen(x);
        if (n == 1)
            return x * y;

        int u = (int)Math.Pow(10, n / 2);

        int a = x / u;
        int b = x % u;

        int c = y / u;
        int d = y % u;

        int ac = RecIntMult(a, c);
        int ad = RecIntMult(a, d);
        int bc = RecIntMult(b, c);
        int bd = RecIntMult(b, d);

        return (int)Math.Pow(10, n) * ac + u * (ad + bc) + bd;

        int GetLen(int num)
        {
            if (num == 0)
                return 1;
            if (num < 0)
                num = -num;

            return (int)Math.Log10(num) + 1;
        }
    }

    public static void MergeSort(int[] a)
    {
        void Merge(int[] a, int mergeFrom, int mergeTo, int middle)
        {
            var leftIndex = mergeFrom;
            var rightIndex = middle;
            var merged = new int[mergeTo - mergeFrom];
                
            for (var i = 0; i < merged.Length; i++)
            {
                if (leftIndex >= middle)
                    merged[i] = a[rightIndex++];
                else if (rightIndex >= mergeTo)
                    merged[i] = a[leftIndex++];
                else
                    merged[i] = a[leftIndex] < a[rightIndex] ? a[leftIndex++] : a[rightIndex++];
            }

            for (int i = 0; i < merged.Length; i++) 
                a[mergeFrom + i] = merged[i];
        }
        
        void Sort(int[] a, int sortFrom, int sortTo)
        {
            if ( sortFrom >= sortTo - 1)
                return;

            var middle = sortFrom + (sortTo - sortFrom) / 2;
        
            Sort(a, sortFrom, middle);
            Sort(a, middle, sortTo);
            Merge(a, sortFrom, sortTo, middle);
        }

        Sort(a, 0, a.Length);
    }

    public static string Karatsuba(string x, string y)
    {
        (string a, string b) SplitNumber(string s, int at) => 
            (s.Substring(0, at), s.Substring(at));
        
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
        
        (string xAligned, string yAligned) AlignTwoPow(string x, string y)
        {
            int NextNearest(int x)
            {
                x--;
                x |= x >> 1;
                x |= x >> 2;
                x |= x >> 4;
                x |= x >> 8;
                x |= x >> 16;
                return x + 1;
            }

            var n = NextNearest(Math.Max(x.Length, y.Length));
            return (x.PadLeft(n, '0'), y.PadLeft(n, '0'));
        }

        string Add(string x, string y)
        {
            var (xAligned, yAligned) = Align(x, y);
            int[] xDigits = ToInt(xAligned.ToCharArray());
            int[] yDigits = ToInt(yAligned.ToCharArray());
            int[] sum = new int[xDigits.Length + 1];
            int carry = 0; 
            
            for (int i = sum.Length - 1; i > 0; i--)
            {
                var ab = xDigits[i - 1] + yDigits[i - 1] + carry;
                sum[i] = ab % 10;
                carry = ab / 10;
            }

            sum[0] = carry;
            var result = "";
            var startIndex = 0;
            for (int i = 0; i < sum.Length; i++)
                if (sum[i] == 0)
                    startIndex++;
                else
                    break;
            for (int i = startIndex; i < sum.Length; i++)
                result += sum[i];

            return result;
        }

        string Sub(string x, string y)
        {
            var (xAligned, yAligned) = Align(x, y);
            int[] xDigits = ToInt(xAligned.ToCharArray());
            int[] yDigits = ToInt(yAligned.ToCharArray());
            int[] sum = new int[xDigits.Length];
            int carry = 0;

            for (int i = sum.Length - 1; i >= 0; i--)
            {
                var ab = xDigits[i] - yDigits[i] - carry;
                sum[i] = ab < 0 ? 10 + ab : ab; 
                carry = ab < 0 ? 1 : 0;
            }

            var result = "";
            var startIndex = 0;
            for (int i = 0; i < sum.Length; i++)
                if (sum[i] == 0)
                    startIndex++;
                else
                    break;
            for (int i = startIndex; i < sum.Length; i++)
                result += sum[i];

            return result;
        }

        string ByTen(string x, int y)
        {
            for (int i = 0; i < y; i++) 
                x += "0";
            return x;
        }

        var (xAl, yAl) = AlignTwoPow(x, y);

        int n = xAl.Length;

        if (n == 0)
            return "0";
        
        if(n <= 1)
            return (int.Parse(xAl) * int.Parse(yAl)).ToString();

        var (a, b) = SplitNumber(xAl, n / 2);
        var (c, d) = SplitNumber(yAl, n / 2);

        var p = Add(a, b);
        var q = Add(c, d);
        var ac = Karatsuba(a, c);

        var bd = Karatsuba(b, d);
        var pq = Karatsuba(p, q);
        var adbc = Sub(Sub(pq, ac), bd);
        return Add(Add(ByTen(ac, n), ByTen(adbc, n / 2)), bd);
    }
}