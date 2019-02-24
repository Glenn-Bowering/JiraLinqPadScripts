<Query Kind="Program">
  <Reference Relative="..\..\source\repos\JiraApi\JiraApi\bin\Debug\JiraApi.dll">C:\Users\facil\source\repos\JiraApi\JiraApi\bin\Debug\JiraApi.dll</Reference>
  <Namespace>Jira</Namespace>
  <Namespace>Jira.Api</Namespace>
</Query>

void Main()
{
	JiraClient jiraClient = new JiraClient(null, null);

	var issueList = jiraClient.LoadMultipleAtlassianJsonFiles(@"C:\Users\facil\OneDrive\Documents\Work\Contracts\ECS Digital\Jira Extracts\ET");

	//var issueList = LoadIssues(@"C:\Users\facil\OneDrive\Documents\Work\Contracts\ECS Digital\Jira Extracts\TRAN");

	issueList.Count.Dump();
	//issueList.Dump();

	var newIssueList = from issue in issueList
						   //where 0 == Math.Round(issue.CycleTime.TotalDays)
					   orderby issue.Created
					   select new
					   {
						   Issue = issue.Key + ": " + issue.IssueType,
						   Created = ((DateTime)issue.Created).ToString("dd/MM/yyyy"),
						   WaitTime = Math.Round(issue.LeadTime.TotalDays) - Math.Round(issue.CycleTime.TotalDays),
						   CycleTime = Math.Round(issue.CycleTime.TotalDays)
					   };


	newIssueList.Dump();
}

// Define other methods and classes here