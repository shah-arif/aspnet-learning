


// string GetData()
// {
//     Thread.Sleep(1000);
//     return "Hello World";
// }

// string result = GetData();

// Console.WriteLine(result);


// static async Task<string> GetDataAsync()
// {
//     await Task.Delay(1000);
//     Console.WriteLine("Loading data...Time: 1 second");
//     await Task.Delay(1000);
//     Console.WriteLine("Loading data...Time:  second");
//     return "Data received";
// }

// string result = await GetDataAsync();

// Console.WriteLine(result);

// // For not return value
// static async Task SaveDataAsync(){
//     await Task.Delay(1000);
// }

// // For a return value
// static async Task<string> TextAsync(){
//     await Task.Delay(1000);
//     return "Data saved";
// }

// string result = await TextAsync();

// Console.WriteLine(result);

// HttpClient httpClient = new();

// async Task<string> GetMessageAsync()
// {
//     // Free fake api to get a random message (https://jsonplaceholder.typicode.com/users)
//     string url = "https://jsonplaceholder.typicode.com/users/1";
//     HttpResponseMessage response = await httpClient.GetAsync(url);
//     string content = await response.Content.ReadAsStringAsync();
//     return content;
// }

// Console.WriteLine("Starting...");

// string message = await GetMessageAsync();

// Console.WriteLine(message);
// Console.WriteLine("Finished");


// static async Task<string> GetDataAsync()
// {
//     Console.WriteLine("Getting data...");

//     await Task.Delay(2000);

//     return "Data received!";
// }

// Console.WriteLine("Starting...");

// string result = await GetDataAsync();

// Console.WriteLine(result);

// Console.WriteLine("Finished.");