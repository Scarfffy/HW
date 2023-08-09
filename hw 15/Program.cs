class Parser
{
    private readonly string _path;

    public Parser(string path)
    {
        this._path = path;
    }

    public async Task Parse()
    {
        var buffer = new byte[500 * 1024 * 1024];
        var bufferResult = new byte[500 * 1024 * 1024];

        using var readSemaphore = new SemaphoreSlim(1);
        using var writeSemaphore = new SemaphoreSlim(1);

        using var stream = File.OpenRead(_path);
        using var resultStream = File.OpenWrite("../../../../result.bin");

        long bytesRead = 0;
        long bytesProcessed = 0;
        while (bytesRead < stream.Length)
        {
            await readSemaphore.WaitAsync();

            var count = await stream.ReadAsync(buffer, 0, buffer.Length);
            bytesRead += count;

            readSemaphore.Release();

            var processTask = ProcessBufferAsync(buffer, bufferResult, count);
            var writeTask = WriteProcessedDataAsync(resultStream, bufferResult, count, writeSemaphore);

            await Task.WhenAll(processTask, writeTask);

            bytesProcessed += count;
            Console.WriteLine($"Processed: {bytesProcessed} / {stream.Length}");
        }
    }

    private async Task ProcessBufferAsync(byte[] inputBuffer, byte[] outputBuffer, int length)
    {
        await Task.Run(() =>
        {
            for (int i = 0; i < length; i++)
            {
                var ch = (char)inputBuffer[i];
                if (char.IsAscii(ch))
                {
                    outputBuffer[i] = inputBuffer[i];
                }
            }
        });
    }

    private async Task WriteProcessedDataAsync(FileStream resultStream, byte[] buffer, int length, SemaphoreSlim writeSemaphore)
    {
        await writeSemaphore.WaitAsync();

        try
        {
            await resultStream.WriteAsync(buffer, 0, length);
        }
        finally
        {
            writeSemaphore.Release();
        }
    }
}
