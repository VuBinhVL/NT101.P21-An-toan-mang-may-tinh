using System.Text;

namespace Bai_2
{
	public class Program
	{
		public static void Main(string[] args)
		{
			bool flag = false;
			int th, k;
			string plaintext, ciphertext, kq;
			// Hỗ trợ Unicode
			Console.OutputEncoding = Encoding.UTF8;
			Console.InputEncoding = Encoding.UTF8;

			Dictionary<char, int> map = new Dictionary<char, int>();
			map = GetDictionaryAlphabet();
			//Tạo menu
			Console.WriteLine("==== Menu ====");
			while (true)
			{
				Console.WriteLine("1. Cho phép nhập khóa k và đoạn plaintext để encrypt.");
				Console.WriteLine("2. Cho phép nhập khóa k và đoạn cyphertext để decrypt.");
				Console.WriteLine("3. Cho nhập doạn cyphertext để decrypt. Với khóa k chạy từ 0 đến 25");
				Console.WriteLine("4. Thoát chương trình");
				Console.Write("Chọn trường hợp: ");
				flag = int.TryParse(Console.ReadLine(), out th);
				while (!flag || th > 4 || th < 1)
				{
					Console.Write("Lỗi. Chọn lại: ");
					flag = int.TryParse(Console.ReadLine(), out th);
				}
				flag = false;
				switch (th)
				{
					case 1:
						Console.Write("Chọn giá trị khóa k: ");
						flag = int.TryParse(Console.ReadLine(), out k);
						while (!flag || k < 0 || k > 25)
						{
							Console.Write("0 <= k <= 25. Chọn lại giá trị khóa: ");
							flag = int.TryParse(Console.ReadLine(), out k);
						}
						flag = false;
						Console.Write("Nhập đoạn văn cần mã hóa: ");
						plaintext = Console.ReadLine();
						if (plaintext == "")
						{
							Console.WriteLine("Không có gì để mã hóa! ");
							break;
						}
						else
							kq = SolveCase1(k, plaintext, map);
						Console.WriteLine("Kết quả: " + kq);
						break;

					case 2:
						Console.Write("Chọn giá trị khóa k: ");
						flag = int.TryParse(Console.ReadLine(), out k);
						while (!flag || k < 0 || k > 25)
						{
							Console.Write("0 <= k <= 25. Chọn lại giá trị khóa: ");
							flag = int.TryParse(Console.ReadLine(), out k);
						}
						flag = false;
						Console.Write("Nhập đoạn văn cần giải mã: ");
						ciphertext = Console.ReadLine();
						if (ciphertext == "")
						{
							Console.WriteLine("Không có gì để giải mã! ");
							break;
						}
						else
							kq = SolveCase2(k, ciphertext, map);
						Console.WriteLine("Kết quả: " + kq);
						break;

					case 3:
						Console.Write("Nhập đoạn văn cần giải mã: ");
						ciphertext = Console.ReadLine();
						if (ciphertext == "")
						{
							Console.WriteLine("Không có gì để giải mã! ");
							break;
						}
						else
							SolveCase3(ciphertext, map);
						break;

					case 4:
						return;

					default:
						break;
				}
			}
		}

		//Hàm tạo dictionary để lưu trữ vị trí các lý tự
		public static Dictionary<char, int> GetDictionaryAlphabet()
		{
			Dictionary<char, int> dic = new Dictionary<char, int>();
			for (int i = 0; i < 26; i++)
			{
				dic[(char)('A' + i)] = i;
			}
			return dic;
		}

		//Hàm giải quyết trường hợp 1
		public static string SolveCase1(int k, string plaintext, Dictionary<char, int> map)
		{
			int p, viTriCipher;
			char cipher;
			string rs = "";
			plaintext = plaintext.ToUpper();
			foreach (char c in plaintext)
			{
				if (c == ' ')
				{
					rs = rs + " ";
				}
				else
				{
					map.TryGetValue(c, out p);
					viTriCipher = (p + k) % 26;
					cipher = map.FirstOrDefault(x => x.Value == viTriCipher).Key;
					rs = rs + cipher;
				}
			}
			return rs;
		}

		//Hàm giải quyết trường hợp 2
		public static string SolveCase2(int k, string ciphertext, Dictionary<char, int> map)
		{
			int p, viTriCipher;
			char plainText;
			string rs = "";
			ciphertext = ciphertext.ToUpper();
			foreach (char c in ciphertext)
			{
				if (c == ' ')
				{
					rs = rs + " ";
				}
				else
				{
					map.TryGetValue(c, out viTriCipher);
					p = (viTriCipher - k + 26) % 26;
					plainText = map.FirstOrDefault(x => x.Value == p).Key;
					rs = rs + plainText;
				}
			}
			return rs;
		}

		//Hàm giải quyết trường hợp 3
		public static void SolveCase3(string ciphertext, Dictionary<char, int> map)
		{
			int p, viTriCipher;
			char plainText;
			string rs = "";
			ciphertext = ciphertext.ToUpper();
			for (int k = 0; k < 26; k++)
			{
				foreach (char c in ciphertext)
				{
					if (c == ' ')
					{
						rs = rs + " ";
					}
					else
					{
						map.TryGetValue(c, out viTriCipher);
						p = (viTriCipher - k + 26) % 26;
						plainText = map.FirstOrDefault(x => x.Value == p).Key;
						rs = rs + plainText;
					}
				}
				Console.WriteLine("Kết quả khi k = " + k + " : " + rs);
				rs = "";
			}
		}
	}
}