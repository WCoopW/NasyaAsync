//public class Request
//{
//    public double app_time;
//    public double sen_time;
//    public Request(double a)
//    {
//        this.app_time = a;
//    }
//}

//public class Program
//{
//    static double GenerateInterarrivalTime(double lambda)
//    {
//        Random rand = new Random();
//        double u = rand.NextDouble();
//        return -Math.Log(u) / lambda;
//    }
//    public static void Main()
//    {
//        List<Request> queue = new List<Request>();
//        List<double> app_time_req = new List<double>();
//        var rnd = new Random();
//        Console.WriteLine("Введите время работы для асинхронной системы:");
//        int work_time = int.Parse(Console.ReadLine());
//        double time = 0;
//        while (time < work_time)
//        {
//            time += GenerateInterarrivalTime(0.4);
//            app_time_req.Add(time);
//        }

//        double curr_time = 0;
//        double time_new_req = 0;
//        for (int i = 1; i <= work_time; i++)
//        {

//        }



//        double[] time_array = new double[N];
//        time_array[0] = GenerateInterarrivalTime(0.4);
//        for (int i = 1; i < N; i++)
//        {
//            time_array[i] = time_array[i - 1] + GenerateInterarrivalTime(0.4);
//        }
//        int max_time = (int)time_array[N] + 1;
//        int[] win_array = new int[max_time];
//        int serv_req = 0;
//        double curr_time = 0;
//        int k = 0;
//        double time_sum = 0;
//        for (int i = 0; i < N; i++)
//        {

//        }


//        Console.WriteLine("lambda\t\tavg_request\t\tavg_time");




//        for (double lambda = 0.001; lambda < 1; lambda += 0.001)
//        {
//            double time_sum = 0;
//            int numRequest = 0;
//            double serv_req = 0;

//            List<Request> queue = new List<Request>();

//            int[] winArr = new int[N];
//            for (int i = 0; i < N; i++)
//            {
//                winArr[i] = generator.Generate(Math.Round(lambda, 3));
//            }
//            for (int i = 0; i < N; i++)
//            {
//                for (int k = 0; k < winArr[i]; k++)
//                {
//                    queue.Add(new Request(rnd.NextDouble() + i));
//                }
//                numRequest += queue.Count;
//                if (queue.Count != 0 && i != 0)
//                {
//                    time_sum += (i + 2) - queue[0].app_time;
//                    queue.RemoveAt(0);
//                    serv_req++;
//                }
//            }

//            double avg_req = Math.Round(numRequest / (double)N, 3);
//            double avg_time = Math.Round(time_sum / serv_req, 3);

//            Console.WriteLine($"{Math.Round(lambda, 3)}\t\t{avg_req}\t\t\t{avg_time}");
//        }
//        Console.ReadKey();
//    }
//}
using System;

public class PoissonGenerator
{
    private Random _random;

    public PoissonGenerator(int seed)
    {
        _random = new Random(seed);
    }

    public int Generate(double lambda)
    {
        double L = Math.Exp(-lambda);
        double p = 1.0;
        int k = 0;

        do
        {
            k++;
            double u = _random.NextDouble();
            p *= u;
        } while (p > L);

        return k - 1;
    }
}

public class Request
{
    public double app_time;
    public double sen_time;
    public Request(double a)
    {
        this.app_time = a;
    }
}


public class Program
{

    public static void Main()
    {
        var rnd = new Random();
        var generator = new PoissonGenerator(123);
        
        Console.WriteLine("Введите время работы для асинхронной системы:");
        int N = int.Parse(Console.ReadLine());
        Console.WriteLine("lambda\t\tavg_request\t\tavg_time");
        for (double lambda = 0.001; lambda < 1; lambda += 0.001)
        {
            double next_send_time = 0;
            double time_sum = 0;
            int numRequest = 0;
            double serv_req = 0;

            List<Request> queue = new List<Request>();

            int[] winArr = new int[N];
            for (int i = 0; i < N; i++)
            {
                winArr[i] = generator.Generate(Math.Round(lambda, 3));
            }
            for (int i = 0; i < N; i++)
            {
                for (int k = 0; k < winArr[i]; k++)
                {
                    queue.Add(new Request(rnd.NextDouble() + i));
                }
                numRequest += queue.Count;
                if (i == 0)
                {
                    if (queue.Count != 0)
                    {
                        next_send_time = queue[0].app_time + 1;
                        queue.RemoveAt(0);
                        time_sum ++;
                    }
                }
                if (queue.Count != 0)
                {
                    if (queue[0].app_time < next_send_time)
                    {
                        time_sum += next_send_time - queue[0].app_time + 1;
                        next_send_time++;
                    }
                    else
                    {
                        time_sum ++;
                        next_send_time = queue[0].app_time + 1;
                    }
                    queue.RemoveAt(0);
                    serv_req++;
                }
            }

            double avg_req = Math.Round(numRequest / (double)N, 3);
            double avg_time = Math.Round(time_sum / serv_req, 3);

            Console.WriteLine($"{Math.Round(lambda, 3)}\t\t{avg_req}\t\t\t{avg_time}");
        }
        Console.ReadKey();
    }
}