using fedir_multitasking_semaphoreslim;

int putCarsCount = 100;
int popCarsCount = 100;
int iteration = 0;
StorageCounter storage = new StorageCounter();
SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
List<Task> cars = new List<Task>(putCarsCount + popCarsCount);

FillCarsTaskListRandomly(cars, putCarsCount, popCarsCount);

Console.WriteLine($"Cars count = {cars.Count}");

await Task.WhenAll(cars);

Console.WriteLine($"Storage items count: {storage.ItemsCount} after {iteration} iterations.");

void FillCarsTaskListRandomly(List<Task> cars, int putCarsCount, int popCarsCount)
{
    for (int i = 0; i < putCarsCount; i++)
    {
        cars.Add(CarEntered(true));
    }
    for (int i = 0; i < popCarsCount; i++)
    {
        cars.Add(CarEntered(false));
    }

    Shuffle(cars);
}

void Shuffle<T>(List<T> list)
{
    Random random = new Random();

    for (int i = list.Count - 1; i > 0; i--)
    {
        int j = random.Next(i + 1);
        (list[i], list[j]) = (list[j], list[i]);
    }
}

async Task CarEntered(bool isIncrement)
{
    await semaphore.WaitAsync();
    try
    {
        iteration++;

        if (isIncrement)
            storage.ItemsCount++;
        else
            storage.ItemsCount--;

        await Task.Delay(50);
    }
    finally
    {
        semaphore.Release();
    }

    Console.WriteLine($"Iteration {iteration}, items count: {storage.ItemsCount}");
}