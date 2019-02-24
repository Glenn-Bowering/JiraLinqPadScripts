<Query Kind="Program">
  <Reference Relative="..\..\source\repos\JiraApi\JiraApi\bin\Debug\JiraApi.dll">C:\Users\facil\source\repos\JiraApi\JiraApi\bin\Debug\JiraApi.dll</Reference>
  <Namespace>Jira</Namespace>
  <Namespace>Jira.Api</Namespace>
  <Namespace>Jira.Api.ObjectModel</Namespace>
</Query>

void Main()
{
	JiraClient jiraClient = new JiraClient(null, null);

	var issueList = jiraClient.LoadMultipleAtlassianJsonFiles(@"C:\Users\facil\OneDrive\Documents\Work\Contracts\ECS Digital\Jira Extracts\TRAN");

	
	issueList.Count.Dump();
	
	
	var cycleTimeDistribution = CalculateCycleTimeDistributionDays(issueList, 90);
	
	cycleTimeDistribution.Dump();
}




Dictionary<int, int> CalculateCycleTimeDistributionDays(List<Jira.Issue> issueList, int daysToCalculate)
{
	Dictionary<int, int> cycleTimeDistribution = new Dictionary<int, int>();

	for (int i = 0; i < daysToCalculate; i++)
	{
		cycleTimeDistribution.Add(i, 0);
	}


	foreach (var issue in issueList)
	{
		int cycleTime = (int)Math.Round(issue.CycleTime.TotalDays);

		if (cycleTimeDistribution.ContainsKey(cycleTime))
		{
			cycleTimeDistribution[cycleTime] += 1;
		}
		else
		{
			Console.Out.WriteLine("Cycle time " + cycleTime + " out of bounds (" + daysToCalculate + "). Issue: " + issue.Key);
		}

	}
	return cycleTimeDistribution;
}