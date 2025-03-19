using System.Text;

namespace Bai_4._2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			Console.InputEncoding = Encoding.UTF8;
			string key, cypherText, plainText, result;
			bool flag;
			int th;
			Console.OutputEncoding = Encoding.UTF8;
			char[,] maTranPlayFair = new char[5, 5];
			//Nhập key
			Console.Write("Nhập khóa: ");
			key = Console.ReadLine();
			key = key.ToUpper().Replace(" ", "").Replace("J", "I");
			Console.WriteLine(key);

			//Tạo ma trận khóa
			maTranPlayFair = TaoMaTranKhoa(maTranPlayFair, key);

			// In ma trận khóa
			Console.WriteLine("Ma trận khóa là: ");
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
					Console.Write(maTranPlayFair[i, j] + " ");
				Console.WriteLine();
			}

			//Tạo menu
			while (true)
			{
				Console.WriteLine("1. Nhập đoạn plaintext để encrypt.");
				Console.WriteLine("2. Nhập đoạn cyphertext để decrypt.");
				Console.WriteLine("3. Thoát chương trình");
				Console.Write("Chọn trường hợp: ");
				flag = int.TryParse(Console.ReadLine(), out th);
				while (!flag || th > 3 || th < 1)
				{
					Console.Write("Lỗi. Chọn lại: ");
					flag = int.TryParse(Console.ReadLine(), out th);
				}
				switch (th)
				{
					case 1:
						Console.Write("Nhập đoạn văn cần mã hóa: ");
						plainText = Console.ReadLine().ToUpper().Replace(" ", "");
						if (plainText == "")
						{
							Console.WriteLine("Không có gì để mã hóa! ");
							break;
						}
						else
							result = MaHoa(plainText, maTranPlayFair);
						Console.WriteLine("Sau khi mã hóa: " + result);
						break;

					case 2:
						Console.Write("Nhập đoạn văn cần giải mã: ");
						cypherText = Console.ReadLine().ToUpper().Replace(" ", "");
						if (cypherText == "")
						{
							Console.WriteLine("Không có gì để giải mã! ");
							break;
						}
						else if (cypherText.Length % 2 != 0)
						{
							Console.WriteLine("CypherText có độ dài là chẵn");
						}
						else if (cypherText.Contains('J'))
						{
							Console.WriteLine("CypherText không được chứa ký tự J");
						}
						else
						{
							result = GiaiMa(cypherText, maTranPlayFair);
							Console.WriteLine("Sau khi giải mã: " + result);
						}
						break;

					case 3:
						break;

					default:
						break;
				}
			};
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

		public static string MaHoa(string plaintText, char[,] matran)
		{
			string rs = "";
			if (plaintText.Length % 2 != 0)
			{
				plaintText += 'X';
			}
			for (int i = 0; i < plaintText.Length; i = i + 2)
			{
				(int row1, int col1) = TimViTri(plaintText[i], matran);
				int y = i + 1;
				(int row2, int col2) = TimViTri(plaintText[y], matran);
				if (row1 == row2) //Cùng hàng
				{
					rs += matran[row1, (col1 + 1) % 5];
					rs += matran[row2, (col2 + 1) % 5];
				}
				else if (col1 == col2) //Cùng cột
				{
					rs += matran[(row1 + 1) % 5, col1];
					rs += matran[(row2 + 1) % 5, col2];
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