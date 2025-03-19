using System.Text;
using System.Text.Unicode;

namespace Bai_4._1
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.UTF8;
			Console.OutputEncoding = Encoding.UTF8;
			char[,] maTranPlayFair = new char[5, 5];
			string key = "Harry Potter";
			string cipherText = "ARYWYPHCBVEBYGMPNCYGCNTDNCWTMGRMFTQPLEWTMLREFBEBQEBIYGBFLPHVOAEHKDHEUNGQFEROLEWTMLOPHEQGOSBEROQDWTLCMTHBWLNRKXRYLORYYPHCBVEBYRLGYDMKYGGWKLROANDBWGNERMNGYRLGHEWRTRLMBRHMUDGVODVTEGMCHLGWCMTFODNRRYCMZKODDUTDXGEOPOYRMFRMGUKXRYGHABROVTGQMCEHPRPEOTSEGEQLARYWYPOTMGQDOEXGOAUDHGUTULTNEHFTFHPGXGVPHGURBDMEGWKLETCBOTNTFQLTAEHMTUGEOAHEVEROXGVPHGDEWTEWGQIEDLPILERWPMOATNGQKQEAHBMVRFKBRMKLXODXFREBHMNUKXRYKLRMFLWDDNCN";
			key = key.ToUpper().Replace(" ", "");
			maTranPlayFair = TaoMaTranKhoa(maTranPlayFair, key);

			// In ma trận khóa
			Console.WriteLine("Ma trận khóa là: ");
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
					Console.Write(maTranPlayFair[i, j] + " ");
				Console.WriteLine();
			}

			// Giải mã
			string plainText = GiaiMa(cipherText, maTranPlayFair);
			Console.WriteLine("Đoạn mã sau khi giải là: " + plainText); *
		}

		public static char[,] TaoMaTranKhoa(char[,] matran, string key)
		{
			HashSet<char> keys = new HashSet<char>();
			List<char> keysList = new List<char>();
			//Điều chỉnh khóa sao cho ko lặp từ và không chứa J
			foreach (char c in key)
			{
				if (c.Equals('J'))
				{
					continue;
				}
				else
					keys.Add(c);
			}

			//Tạo mảng chứa khóa và các ký từ A đến Z thỏa ma trận FairPlay
			for (char i = 'A'; i <= 'Z'; i++)
			{
				if (i.Equals('J'))
				{
					continue;
				}
				else
					keys.Add(i);
			}
			//Chuyển HashSet sang List
			keysList = keys.ToList();
			int vitri = 0;
			//Tạo ma trận khóa
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
					matran[i, j] = keysList[vitri++];
			}

			return matran;
		}

		public static (int, int) TimViTri(char c, char[,] matran)
		{
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (matran[i, j] == c)
						return (i, j);
				}
			}
			return (-1, -1);
		}

		public static string GiaiMa(string cipherText, char[,] matran)
		{
			string rs = "";
			for (int i = 0; i < cipherText.Length; i = i + 2)
			{
				(int row1, int col1) = TimViTri(cipherText[i], matran);
				int y = i + 1;
				(int row2, int col2) = TimViTri(cipherText[y], matran);
				if (row1 == row2) //Cùng hàng
				{
					rs += matran[row1, (col1 + 4) % 5];
					rs += matran[row2, (col2 + 4) % 5];
				}
				else if (col1 == col2) //Cùng cột
				{
					rs += matran[(row1 + 4) % 5, col1];
					rs += matran[(row2 + 4) % 5, col2];
				}
				else
				{
					rs += matran[row1, col2];
					rs += matran[row2, col1];
				}
				rs = rs + " ";
			}
			return rs;
		}
	}
}