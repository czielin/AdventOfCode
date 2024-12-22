var lines = File.ReadAllLines("input.txt");
Dictionary<int, List<int>> pageOrders = new Dictionary<int, List<int>>();
Dictionary<int, List<int>> reversePageOrders = new Dictionary<int, List<int>>();
int middleSum = 0;
int correctedMiddleSum = 0;
foreach (var line in lines)
{
    if (line.Contains("|"))
    {
        var orderParts = line.Split('|');
        int earlyPage = int.Parse(orderParts[0]);
        int laterPage = int.Parse(orderParts[1]);
        if (!pageOrders.ContainsKey(earlyPage))
        {
            pageOrders[earlyPage] = new List<int>();
        }
        pageOrders[earlyPage].Add(laterPage);
        if (!reversePageOrders.ContainsKey(laterPage))
        {
            reversePageOrders[laterPage] = new List<int>();
        }
        reversePageOrders[laterPage].Add(earlyPage);
    }
    else if (line.Contains(","))
    {
        bool orderIsValid = true;
        var pageNumbers = line.Split(",").Select(p => int.Parse(p)).ToList();
        for (int currentPage = 1; currentPage < pageNumbers.Count; currentPage++)
        {
            int currentPageNumber = pageNumbers[currentPage];
            if (pageOrders.ContainsKey(currentPageNumber))
            {
                var invalidPageNumbers = pageOrders[currentPageNumber];
                var earlierPages = pageNumbers.Take(currentPage).ToList();
                if (invalidPageNumbers.Exists(p => earlierPages.Contains(p)))
                {
                    orderIsValid = false;
                }
            }
        }
        if (orderIsValid)
        {
            middleSum += pageNumbers[pageNumbers.Count / 2];
        }
        else
        {
            var correctedPageNumbers = new List<int>(pageNumbers);
            correctedPageNumbers.Sort(PageComparer);
            correctedMiddleSum += correctedPageNumbers[pageNumbers.Count / 2];
        }
    }
}
Console.WriteLine($"The sum of middle pages for valid sequences is: {middleSum}");
Console.WriteLine($"The sum of middle pages for corrected sequences is: {correctedMiddleSum}");
Console.ReadLine();

int PageComparer(int page1, int page2)
{
    int returnValue = 0;
    if (pageOrders.ContainsKey(page1))
    {
        var invalidPageNumbers = pageOrders[page1];
        if (invalidPageNumbers.Contains(page2)) 
        {
            returnValue = -1; 
        }
    }
    if (pageOrders.ContainsKey(page2))
    {
        var invalidPageNumbers = pageOrders[page2];
        if (invalidPageNumbers.Contains(page1))
        {
            returnValue = 1;
        }
    }
    if (reversePageOrders.ContainsKey(page1))
    {
        var invalidPageNumbers = reversePageOrders[page1];
        if (invalidPageNumbers.Contains(page2)) 
        {
            returnValue = 1; 
        }
    }
    if (reversePageOrders.ContainsKey(page2))
    {
        var invalidPageNumbers = reversePageOrders[page2];
        if (invalidPageNumbers.Contains(page1))
        {
            returnValue = -1;
        }
    }
    return returnValue;
}