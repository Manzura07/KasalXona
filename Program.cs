using System.Text.RegularExpressions;

class Program
{
    static Queue<(string Ism, string Telefon)> navbat = new Queue<(string, string)>();

    static void Main()
    {
        int tanlov;
        do
        {
            Console.WriteLine("------------- NAVBAT -------------");
            Console.WriteLine("Kerakli menuni tanlang!");
            Console.WriteLine("1. Navbatga qo'shish");
            Console.WriteLine("2. Navbatni ko'rish");
            Console.WriteLine("3. Keyingi odamni ko'rish");
            Console.WriteLine("4. Keyingi odamni qabul qilish");
            Console.WriteLine("0. Chiqib ketish");
            Console.WriteLine("----------------------------------");
            Console.Write("Tanlang: ");

            if (!int.TryParse(Console.ReadLine(), out tanlov))
            {
                Console.WriteLine("Noto'g'ri tanlov! Qayta urining.\n");
                continue;
            }

            switch (tanlov)
            {
                case 1:
                    NavbatgaQoshish();
                    break;
                case 2:
                    NavbatniChopEtish();
                    break;
                case 3:
                    KeyingiOdamniKorish();
                    break;
                case 4:
                    KeyingiOdamniQabulQilish();
                    break;
                case 0:
                    Console.WriteLine("Dasturdan chiqildi.");
                    break;
                default:
                    Console.WriteLine("Noto'g'ri tanlov! Qayta urining.\n");
                    break;
            }
        } while (tanlov != 0);
    }

    static void NavbatgaQoshish()
    {
        Console.WriteLine("\nNavbatga qo'shishni tanladingiz.");

        string ism;
        do
        {
            Console.Write("Ismni kiriting: ");
            ism = Console.ReadLine()!;
            if (ism.Length < 5 || ism.Length > 20)
            {
                Console.WriteLine("Ism 5~20 belgidan iborat bo'lishi kerak");
            }
        } while (ism.Length < 5 || ism.Length > 20);

        string telefon;
        Regex telefonFormat = new Regex(@"^\+998 \d{2} \d{3} \d{2} \d{2}$");
        do
        {
            Console.Write("Telefon raqamni kiriting: ");
            telefon = Console.ReadLine()!;

            if (!telefonFormat.IsMatch(telefon))
            {
                Console.WriteLine("Telefon raqam +998 XX XXX XX XX formatda bo'lishi shart!");
            }
            else if (TelefonRaqamMavjud(telefon))
            {
                Console.WriteLine("Bu telefon raqam allaqachon navbatda mavjud!");
                telefon = null;
            }
        } while (telefon == null || !telefonFormat.IsMatch(telefon));

        navbat.Enqueue((ism, telefon));
        Console.WriteLine($"{ism} navbatga qo'shildi.\n");
    }

    static bool TelefonRaqamMavjud(string telefon)
    {
        foreach (var item in navbat)
        {
            if (item.Telefon == telefon) return true;
        }
        return false;
    }

    static void NavbatniChopEtish()
    {
        if (navbat.Count == 0)
        {
            Console.WriteLine("\nNavbatda hech kim yo'q!\n");
            return;
        }

        Console.WriteLine("\nNavbatdagilar");
        Console.WriteLine("----------------------------------");
        int raqam = 1;
        foreach (var odam in navbat)
        {
            Console.WriteLine($"{raqam}. {odam.Ism} {ShifrlanganTelefon(odam.Telefon)}");
            raqam++;
        }
        Console.WriteLine("----------------------------------\n");
    }

    static string ShifrlanganTelefon(string telefon)
    {
        return $"{telefon.Substring(0, 6)}** *** {telefon.Substring(12)}";
    }

    static void KeyingiOdamniKorish()
    {
        if (navbat.Count == 0)
        {
            Console.WriteLine("\nNavbat bo'sh!\n");
            return;
        }

        var keyingi = navbat.Peek();
        Console.WriteLine("\nNavbatdagi inson");
        Console.WriteLine("----------------------------------");
        Console.WriteLine($"{keyingi.Ism} {ShifrlanganTelefon(keyingi.Telefon)}");
        Console.WriteLine("----------------------------------\n");
    }

    static void KeyingiOdamniQabulQilish()
    {
        if (navbat.Count == 0)
        {
            Console.WriteLine("\nNavbat bo'sh!\n");
            return;
        }

        var qabulQilingan = navbat.Dequeue();
        Console.WriteLine($"\n{qabulQilingan.Ism} {ShifrlanganTelefon(qabulQilingan.Telefon)} qabul qilindi.\n");
    }
}
