using Microsoft.AspNetCore.Mvc;
using System;
using KSPLogin.Models;
using System.Net.Sockets;
using System.Net;
using DataAccess;
using DataAccess.Entities;
using System.Data;
using AutoMapper;
using Live_ConsultingKSP.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Live_ConsultingKSP.Controllers
{
    public class TimesheetController : Controller
    {
        private readonly IMapper _mapper;

        Service s = new Service();
        Utils u = new Utils();
        public TimesheetController(IMapper mapper)
        {
            _mapper = mapper;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}


        [CheckCookie("UserUUID")]

		public IActionResult ManageTimesheet()
		{
			TimesheetMdl TM = new TimesheetMdl();
			string CmpUUID = Request.Cookies["CmpUUID"].ToString();
			string EnvUUID = Request.Cookies["EnvUUID"].ToString();
			string UserUUID = Request.Cookies["UserUUID"].ToString();

			Master_Employee ME = s.MasterEmployee.Get()
				.Where(c => c.IsActive == true && c.IsLoginActive == true && c.UUID == UserUUID)
				.FirstOrDefault();

			if (ME == null)
			{
				return NotFound("Employee not found.");
			}

			TM.EmpName = ME.FirstName + " " + ME.LastName;
			TM.EmpUUID = UserUUID;

			// Get the current date
			DateTime today = DateTime.Now;

			// Determine the start and end of the week (Monday to Sunday)
			int diff = today.DayOfWeek == DayOfWeek.Sunday ? -6 : (int)DayOfWeek.Monday - (int)today.DayOfWeek;
			DateTime startOfWeek = today.AddDays(diff);
			DateTime endOfWeek = startOfWeek.AddDays(6);

			string StartOfWeek = startOfWeek.ToString("yyyy-MM-dd");
			string EndOfWeek = endOfWeek.ToString("yyyy-MM-dd");

			TM.DateRangeV = StartOfWeek + " to " + EndOfWeek;
			TM.From_Date = StartOfWeek;
			TM.To_Date = EndOfWeek;

			Timesheet_Header TH = s.TimesheetHeader.Get()
				.Where(c => c.Emp_UUID == UserUUID && c.IsActive == true
					   && c.From_Date == Convert.ToDateTime(StartOfWeek)
					   && c.To_Date == Convert.ToDateTime(EndOfWeek))
				.FirstOrDefault();

			if (TH == null)
			{
				// If the Timesheet Header does not exist, create a new one
				TH = new Timesheet_Header
				{
					UUID = u.GetUUID(),
					Emp_UUID = UserUUID,
					From_Date = Convert.ToDateTime(StartOfWeek),
					To_Date = Convert.ToDateTime(EndOfWeek),
					Total_Hours = "00:00",
					Timesheet_Status = "Pending",
					IsActive = true
				};

				// Save the new Timesheet Header to the database
				s.TimesheetHeader.Add(TH);
				//s.SaveChanges();
			}

			TM.HeaderUUID = TH.UUID;
			TM.Total_Hours = TH.Total_Hours;
			TM.Timesheet_Status = TH.Timesheet_Status;

			// Check if there are any existing timesheet lines
			bool hasTimesheetLines = s.TimesheetLine.Get().Any(l => l.TimesheetHeader_UUID == TH.UUID && l.IsActive == true);

			ViewBag.TmSt = TM;

			if (hasTimesheetLines)
			{
				// Generate HTML table only if timesheet lines exist
				ViewBag.Tbl = GenerateHtmlTable(TM.HeaderUUID);
			}
			else
			{
				ViewBag.Tbl = string.Empty;
				ViewBag.Tbl1 = string.Empty;
			}

			return View();
		}


		[CheckCookie("UserUUID")]
        [HttpGet]
        public IActionResult GetProjectLst(string id)
        {

            string UserUUID = Request.Cookies["UserUUID"].ToString();
            List<Project_CreateProjectPhase> PCPP = s.ProjectCreateProjectPhase.Get()
            .Where(p => p.Select_Team_Member_UUID.Split(',').Contains(UserUUID))
            .DistinctBy(i => i.Project_UUID).ToList();
            List<Project_CreateProject> PCP = new List<Project_CreateProject>();

            for (int i = 0; i < PCPP.Count(); i++)
            {
                Project_CreateProject PC = s.ProjectCreateProject.Get().Where(c => c.IsActive == true && c.UUID == PCPP[i].Project_UUID && c.Project_Status_UUID != "Completed").FirstOrDefault();
                if (PC != null)
                {
                    PCP.Add(PC);
                }

            }

            var states = PCP
              .Select(s => new SelectListItem
              {
                  Value = s.UUID.ToString(), // Ensure Uuid is not null
                  Text = s.Project_Title ?? "Unknown" // Ensure StateName is not null
              })
              .ToList();

            return Json(states);

        }
        [CheckCookie("UserUUID")]
        [HttpGet]
        public IActionResult GetProjectTskLst(string id)
        {
            string UserUUID = Request.Cookies["UserUUID"].ToString();
            List<Project_CreateProjectPhase> PCPP = s.ProjectCreateProjectPhase.Get()
            .Where(p => p.Select_Team_Member_UUID.Split(',').Contains(UserUUID) && p.Project_UUID == id)
            .ToList();
            List<Project_ProjectTask> PPT = new List<Project_ProjectTask>();
            for (int i = 0; i < PCPP.Count(); i++)
            {
                List<Project_ProjectTask> Pt = s.ProjectProjectTask.Get()
                    .Where(c => c.ProjectPhase_UUID == PCPP[i].UUID && c.Project_UUID == PCPP[i].Project_UUID && c.IsActive == true)
                    .ToList();
                if (Pt.Count > 0)
                {
                    PPT.AddRange(Pt);
                }
            }

            var states = PPT
              .Select(s => new SelectListItem
              {
                  Value = s.UUID.ToString(), // Ensure Uuid is not null
                  Text = s.Task_Title ?? "Unknown" // Ensure StateName is not null
              })
              .ToList();

            return Json(states);
        }



		//	public string GenerateHtmlTable(string timesheetHeaderUuid)
		//	{
		//		StringBuilder sb = new StringBuilder();

		//		// Fetch timesheet lines from the database
		//		var timesheetLines = s.TimesheetLine.Get()
		//			.Where(tl => tl.TimesheetHeader_UUID == timesheetHeaderUuid)
		//			.ToList();

		//		if (!timesheetLines.Any())
		//		{
		//			return "<p>No timesheet data found.</p>";
		//		}

		//		// Fetch project names
		//		var projectNames = s.ProjectCreateProject.Get()
		//			.ToDictionary(p => p.UUID, p => p.Project_Title);

		//		// Fetch task names
		//		var taskNames = s.ProjectProjectTask.Get()
		//			.ToDictionary(t => t.UUID, t => t.Task_Title);

		//		// Day mapping dictionary
		//		Dictionary<string, string> dayMapping = new Dictionary<string, string>
		//{
		//	{ "Monday", "Mon" },
		//	{ "Tuesday", "Tue" },
		//	{ "Wednesday", "Wed" },
		//	{ "Thursday", "Thur" },
		//	{ "Friday", "Fri" },
		//	{ "Saturday", "Sat" },
		//	{ "Sunday", "Sun" }
		//};

		//		// Start building the table
		//		sb.AppendLine("<table id=\"buttons-datatables\" class=\"display table table-bordered\" style=\"width:100%;\">");
		//		sb.AppendLine("    <thead>");
		//		sb.AppendLine("        <tr>");
		//		sb.AppendLine("            <th style=\"width:2%\">");
		//		sb.AppendLine("                <input class=\"form-check-input\" type=\"checkbox\" id=\"selectAll\">");
		//		sb.AppendLine("                <button class=\"btn btn-sm btn-outline-dark salesorder\" style=\"padding:3px\" id=\"sa-forward\">");
		//		sb.AppendLine("                    <i class=\"ri-share-forward-2-line\"></i>");
		//		sb.AppendLine("                    <span class=\"tooltiptext\">Forward Task to Next Week</span>");
		//		sb.AppendLine("                </button>");
		//		sb.AppendLine("            </th>");
		//		sb.AppendLine("            <th style=\"width:5%\">Actions</th>"); // Actions column moved to the front
		//		sb.AppendLine("            <th style=\"width:30%\">Project . Task</th>");

		//		// Generate headers for each day of the week
		//		string[] weekdays = { "Mon", "Tue", "Wed", "Thur", "Fri", "Sat", "Sun" };
		//		DateTime startDate = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);

		//		foreach (var day in weekdays)
		//		{
		//			sb.AppendLine($"<th class=\"table-th-weekday\">{day}<div>{startDate:dd/MM}</div></th>");
		//			startDate = startDate.AddDays(1);
		//		}

		//		sb.AppendLine("        </tr>");
		//		sb.AppendLine("    </thead>");
		//		sb.AppendLine("    <tbody>");

		//		// Group timesheet entries by Project and Task
		//		var groupedLines = timesheetLines
		//			.GroupBy(tl => new { tl.Project_UUID, tl.Task_UUID })
		//			.ToList();

		//		foreach (var group in groupedLines)
		//		{
		//			string projectName = projectNames.ContainsKey(group.Key.Project_UUID) ? projectNames[group.Key.Project_UUID] : "Unknown Project";
		//			string taskName = taskNames.ContainsKey(group.Key.Task_UUID) ? taskNames[group.Key.Task_UUID] : "Unknown Task";

		//			sb.AppendLine("        <tr>");
		//			sb.AppendLine("            <td style=\"display:flex; grid-gap:4px;\">");
		//			sb.AppendLine($"                <input class=\"form-check-input check\" type=\"checkbox\" id=\"select-{group.First().ID}\">");
		//			sb.AppendLine("            </td>");

		//			// Action buttons (Edit & Forward Task) placed **before** project/task name
		//			sb.AppendLine("            <td>");
		//			sb.AppendLine($"                <button class=\"btn btn-sm btn-warning edit-task\" data-id=\"{group.First().ID}\">");
		//			sb.AppendLine("                    <i class=\"ri-pencil-line\"></i>");
		//			sb.AppendLine("                </button>");
		//			sb.AppendLine($"                <button class=\"btn btn-sm btn-info forward-task\" data-id=\"{group.First().ID}\">");
		//			sb.AppendLine("                    <i class=\"ri-arrow-right-line\"></i>");
		//			sb.AppendLine("                </button>");
		//			sb.AppendLine("            </td>");

		//			sb.AppendLine($"            <td>{projectName} - {taskName}</td>");

		//			// Insert time entries for each day
		//			foreach (var day in weekdays)
		//			{
		//				var timesheetLine = group.FirstOrDefault(tl => dayMapping.ContainsKey(tl.Day_of_Week) && dayMapping[tl.Day_of_Week] == day);

		//				string hoursForDay = timesheetLine != null ? timesheetLine.Hours : "00:00";
		//				string id = timesheetLine?.ID.ToString() ?? "";
		//				string uuid = timesheetLine?.UUID ?? "";
		//				string remark = timesheetLine?.Remark ?? "";

		//				sb.AppendLine("            <td>");
		//				sb.AppendLine($"                <input type=\"text\" class=\"form-control time-input text-border cleave-time-format\" value=\"{hoursForDay}\" fdprocessedid=\"{Guid.NewGuid()}\">");
		//				sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-id\" value=\"{id}\">");
		//				sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-uuid\" value=\"{uuid}\">");
		//				sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-remark\" value=\"{remark}\">");
		//				sb.AppendLine("            </td>");
		//			}

		//			sb.AppendLine("        </tr>");
		//		}

		//		sb.AppendLine("    </tbody>");
		//		sb.AppendLine("    <tfoot>");
		//		sb.AppendLine("        <tr>");
		//		sb.AppendLine("            <th></th>");
		//		sb.AppendLine("            <th></th>"); // Empty cell for Actions column in footer
		//		sb.AppendLine("            <th>Total Hours</th>");

		//		// Generate empty total input fields for each day
		//		for (int i = 0; i < weekdays.Length; i++)
		//		{
		//			sb.AppendLine("            <th><input type=\"text\" class=\"form-control cleave-time-format\" placeholder=\"hh:mm\" disabled></th>");
		//		}

		//		sb.AppendLine("        </tr>");
		//		sb.AppendLine("    </tfoot>");
		//		sb.AppendLine("</table>");

		//		return sb.ToString();
		//	}

		public string GenerateHtmlTable(string timesheetHeaderUUID)
		{
			StringBuilder sb = new StringBuilder();

			// Fetch timesheet lines
			var timesheetLines = s.TimesheetLine.Get()
				.Where(tl => tl.TimesheetHeader_UUID == timesheetHeaderUUID)
				.ToList();

			if (!timesheetLines.Any())
			{
				return "<p>No timesheet data found.</p>";
			}

			// Fetch project and task names
			var projectNames = s.ProjectCreateProject.Get()
				.ToDictionary(p => p.UUID, p => p.Project_Title);

			var taskNames = s.ProjectProjectTask.Get()
				.ToDictionary(t => t.UUID, t => t.Task_Title);

			// Day mapping dictionary
			Dictionary<string, string> dayMapping = new Dictionary<string, string>
	{
		{ "monday", "mon" }, { "tuesday", "tue" }, { "wednesday", "wed" },
		{ "thursday", "thur" }, { "friday", "fri" }, { "saturday", "sat" }, { "sunday", "sun" }
	};

			sb.AppendLine("<table id=\"buttons-datatables\" class=\"display table table-bordered\" style=\"width:100%;\">");
			sb.AppendLine("    <thead>");
			sb.AppendLine("        <tr>");
			sb.AppendLine("            <th style=\"width:2%\"><input class=\"form-check-input\" type=\"checkbox\" id=\"selectAll\"></th>");
			sb.AppendLine("            <th style=\"width:5%\">Actions</th>");
			sb.AppendLine("            <th style=\"width:30%\">Project . Task</th>");

			string[] weekdays = { "mon", "tue", "wed", "thur", "fri", "sat", "sun" };
			DateTime startDate = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);

			foreach (var day in weekdays)
			{
				sb.AppendLine($"<th class=\"table-th-weekday\">{day}<div>{startDate:dd/MM}</div></th>");
				startDate = startDate.AddDays(1);
			}

			sb.AppendLine("        </tr>");
			sb.AppendLine("    </thead>");
			sb.AppendLine("    <tbody>");

			var groupedLines = timesheetLines.GroupBy(tl => new { tl.Project_UUID, tl.Task_UUID });

			foreach (var group in groupedLines)
			{
				string projectName = projectNames.TryGetValue(group.Key.Project_UUID, out var pName) ? pName : "Unknown Project";
				string taskName = taskNames.TryGetValue(group.Key.Task_UUID, out var tName) ? tName : "Unknown Task";

				sb.AppendLine("        <tr>");
				sb.AppendLine("            <td><input class=\"form-check-input check\" type=\"checkbox\"></td>");
				sb.AppendLine("            <td>");
				sb.AppendLine($"                <button class=\"btn btn-sm btn-warning edit-task\" data-id=\"{group.First().ID}\"><i class=\"ri-pencil-line\"></i></button>");
				sb.AppendLine("            </td>");
				sb.AppendLine($"            <td>{projectName} - {taskName}</td>");

				foreach (var day in weekdays)
				{
					var timesheetLine = group.FirstOrDefault(tl => dayMapping.TryGetValue(tl.Day_of_Week, out var mappedDay) && mappedDay == day);
					string hoursForDay = timesheetLine?.Hours ?? "00:00";
					string remark = timesheetLine?.Remark ?? "";
					string timesheetId = timesheetLine?.ID.ToString() ?? "0";
					string timesheetUuid = timesheetLine?.UUID ?? "";

					sb.AppendLine("            <td>");
					sb.AppendLine($"                <input type=\"text\" class=\"form-control time-input\" value=\"{hoursForDay}\"/>");
					sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-id\" value=\"{timesheetId}\"/>");
					sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-uuid\" value=\"{timesheetUuid}\"/>");
					sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-remark\" value=\"{remark}\"/>");
					sb.AppendLine("            </td>");
				}

				sb.AppendLine("        </tr>");
			}

			sb.AppendLine("    </tbody>");
			sb.AppendLine("</table>");

			return sb.ToString();
		}








		public class TimesheetUpdateModel
		{
			public string Id { get; set; }
			public string UUID { get; set; }
			public string Hours { get; set; }
			public string Remark { get; set; }
			public string HeaderUUID { get; set; }
		}

		[HttpPost]
		public IActionResult UpdateTimesheetLine([FromBody] TimesheetUpdateModel model)
		{
			if (model == null || string.IsNullOrEmpty(model.UUID) || string.IsNullOrEmpty(model.HeaderUUID))
			{
				return BadRequest("Invalid data received.");
			}

			// Fetch the timesheet line by UUID
			var timesheetLine = s.TimesheetLine.Get()
	.FirstOrDefault(tl => decimal.TryParse(model.Id, out decimal parsedId) && tl.ID == parsedId);


			if (timesheetLine == null)
			{
				return NotFound("Timesheet entry not found.");
			}

			// Convert new updated hours to TimeSpan
			TimeSpan newHours = TimeSpan.Zero;
			if (TimeSpan.TryParse(model.Hours, out TimeSpan parsedNewHours))
			{
				newHours = parsedNewHours;
			}

			// Update Hours and Remark in TimesheetLine
			timesheetLine.Hours = model.Hours;
			timesheetLine.Remark = model.Remark;

			// Save changes
			s.TimesheetLine.Update(timesheetLine);

			// Update Total Hours in TimesheetHeader (Adding New Hours)
			var timesheetHeader = s.TimesheetHeader.Get().FirstOrDefault(th => th.UUID == model.HeaderUUID);
			if (timesheetHeader != null)
			{
				// Convert total hours of timesheetHeader to TimeSpan
				TimeSpan totalHours = TimeSpan.Zero;
				if (TimeSpan.TryParse(timesheetHeader.Total_Hours, out TimeSpan parsedTotalHours))
				{
					totalHours = parsedTotalHours;
				}

				// **Simply Add the New Hours** to Total Hours
				totalHours += newHours;

				timesheetHeader.Total_Hours = totalHours.ToString(@"hh\:mm"); // Format as HH:MM

				s.TimesheetHeader.Update(timesheetHeader);
			}

			return Ok(new { message = "Timesheet updated successfully!" });
		}

		[HttpPost]
		public IActionResult UpdateTimesheetStatus([FromBody] string timesheetHeaderUUID)
		{
			if (string.IsNullOrEmpty(timesheetHeaderUUID))
			{
				return BadRequest("Invalid UUID received.");
			}

			// Fetch the timesheet header by UUID
			var timesheetHeader = s.TimesheetHeader.Get().FirstOrDefault(th => th.UUID == timesheetHeaderUUID);

			if (timesheetHeader == null)
			{
				return NotFound("Timesheet Header not found.");
			}

			// Update status to "Submitted"
			timesheetHeader.Timesheet_Status = "Submitted";

			// Save changes
			s.TimesheetHeader.Update(timesheetHeader);

			return Ok(new { message = "Timesheet status updated successfully!" });
		}




		//[HttpPost]
		//public IActionResult UpdateTimesheetLine(string Id, string UUID, string Hours, string Remark, string HeaderUUID)
		//{
		//	if (string.IsNullOrEmpty(UUID) || string.IsNullOrEmpty(HeaderUUID))
		//	{
		//		return BadRequest("Invalid data received.");
		//	}

		//	// Fetch the timesheet line by UUID
		//	var timesheetLine = s.TimesheetLine.Get().FirstOrDefault(tl => tl.UUID == UUID);

		//	if (timesheetLine == null)
		//	{
		//		return NotFound("Timesheet entry not found.");
		//	}

		//	// Convert new updated hours to TimeSpan
		//	TimeSpan newHours = TimeSpan.Zero;
		//	if (TimeSpan.TryParse(Hours, out TimeSpan parsedNewHours))
		//	{
		//		newHours = parsedNewHours;
		//	}

		//	// Update Hours and Remark in TimesheetLine
		//	timesheetLine.Hours = Hours;
		//	timesheetLine.Remark = Remark;

		//	// Save changes
		//	s.TimesheetLine.Update(timesheetLine);

		//	// Update Total Hours in TimesheetHeader (Adding New Hours)
		//	var timesheetHeader = s.TimesheetHeader.Get().FirstOrDefault(th => th.UUID == HeaderUUID);
		//	if (timesheetHeader != null)
		//	{
		//		// Convert total hours of timesheetHeader to TimeSpan
		//		TimeSpan totalHours = TimeSpan.Zero;
		//		if (TimeSpan.TryParse(timesheetHeader.Total_Hours, out TimeSpan parsedTotalHours))
		//		{
		//			totalHours = parsedTotalHours;
		//		}

		//		// **Simply Add the New Hours** to Total Hours
		//		totalHours += newHours;

		//		timesheetHeader.Total_Hours = totalHours.ToString(@"hh\:mm"); // Format as HH:MM

		//		s.TimesheetHeader.Update(timesheetHeader);
		//	}

		//	return Ok(new { message = "Timesheet updated successfully!" });
		//}



		//public string GenerateHtmlTable(string timesheetHeaderUuid)
		//{
		//	StringBuilder sb = new StringBuilder();

		//	// Fetch timesheet lines from the database
		//	var timesheetLines = s.TimesheetLine.Get()
		//		.Where(tl => tl.TimesheetHeader_UUID == timesheetHeaderUuid)
		//		.ToList();

		//	if (!timesheetLines.Any())
		//	{
		//		return "<p>No timesheet data found.</p>";
		//	}

		//	// Fetch project names
		//	var projectNames = s.ProjectCreateProject.Get()
		//		.ToDictionary(p => p.UUID, p => p.Project_Title);  // Assuming Project has UUID and Title

		//	// Fetch task names
		//	var taskNames = s.ProjectProjectTask.Get()
		//		.ToDictionary(t => t.UUID, t => t.Task_Title);  // Assuming Task has UUID and Title

		//	// Start building the table
		//	sb.AppendLine("<table id=\"buttons-datatables\" class=\"display table table-bordered\" style=\"width:100%;\">");
		//	sb.AppendLine("    <thead>");
		//	sb.AppendLine("        <tr>");
		//	sb.AppendLine("            <th style=\"width:2%\">");
		//	sb.AppendLine("                <input class=\"form-check-input\" type=\"checkbox\" id=\"selectAll\">");
		//	sb.AppendLine("                <button class=\"btn btn-sm btn-outline-dark salesorder\" style=\"padding:3px\" id=\"sa-forward\">");
		//	sb.AppendLine("                    <i class=\"ri-share-forward-2-line\"></i>");
		//	sb.AppendLine("                    <span class=\"tooltiptext\">Forward Task to Next Week</span>");
		//	sb.AppendLine("                </button>");
		//	sb.AppendLine("            </th>");
		//	sb.AppendLine("            <th style=\"width:30%\">Project . Task</th>");

		//	// Generate headers for each day of the week
		//	string[] weekdays = { "Mon", "Tue", "Wed", "Thur", "Fri", "Sat", "Sun" };
		//	DateTime startDate = DateTime.Now.Date.AddDays(-(int)DateTime.Now.DayOfWeek + (int)DayOfWeek.Monday);

		//	foreach (var day in weekdays)
		//	{
		//		sb.AppendLine($"<th class=\"table-th-weekday\">{day}<div>{startDate:dd/MM}</div></th>");
		//		startDate = startDate.AddDays(1);
		//	}

		//	sb.AppendLine("        </tr>");
		//	sb.AppendLine("    </thead>");
		//	sb.AppendLine("    <tbody>");

		//	// Group timesheet entries by Project and Task
		//	var groupedLines = timesheetLines
		//		.GroupBy(tl => new { tl.Project_UUID, tl.Task_UUID })
		//		.ToList();

		//	foreach (var group in groupedLines)
		//	{
		//		string projectName = projectNames.ContainsKey(group.Key.Project_UUID) ? projectNames[group.Key.Project_UUID] : "Unknown Project";
		//		string taskName = taskNames.ContainsKey(group.Key.Task_UUID) ? taskNames[group.Key.Task_UUID] : "Unknown Task";

		//		sb.AppendLine("        <tr>");
		//		sb.AppendLine("            <td style=\"display:flex; grid-gap:4px;\">");
		//		sb.AppendLine($"                <input class=\"form-check-input check\" type=\"checkbox\" id=\"select-{group.First().ID}\">");
		//		sb.AppendLine("            </td>");
		//		sb.AppendLine($"            <td>{projectName} - {taskName}</td>");

		//		// Insert time entries for each day
		//		foreach (var day in weekdays)
		//		{
		//			var timesheetLine = group.FirstOrDefault(tl => tl.Day_of_Week == day);
		//			string hoursForDay = timesheetLine != null ? timesheetLine.Hours : "00:00";
		//			string id = timesheetLine != null ? timesheetLine.ID.ToString() : "";
		//			string uuid = timesheetLine != null ? timesheetLine.UUID : "";
		//			string remark = timesheetLine != null ? timesheetLine.Remark : "";  // Assuming Remark exists in TimesheetLine

		//			sb.AppendLine("            <td>");
		//			sb.AppendLine($"                <input type=\"text\" class=\"form-control time-input text-border cleave-time-format\" placeholder=\"{hoursForDay}\" fdprocessedid=\"{Guid.NewGuid()}\">");
		//			sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-id\" value=\"{id}\">");
		//			sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-uuid\" value=\"{uuid}\">");
		//			sb.AppendLine($"                <input type=\"hidden\" class=\"timesheet-remark\" value=\"{remark}\">");
		//			sb.AppendLine("            </td>");
		//		}

		//		sb.AppendLine("        </tr>");
		//	}

		//	sb.AppendLine("    </tbody>");
		//	sb.AppendLine("    <tfoot>");
		//	sb.AppendLine("        <tr>");
		//	sb.AppendLine("            <th></th>");
		//	sb.AppendLine("            <th>Total Hours</th>");

		//	// Generate empty total input fields for each day
		//	for (int i = 0; i < weekdays.Length; i++)
		//	{
		//		sb.AppendLine("            <th><input type=\"text\" class=\"form-control cleave-time-format\" placeholder=\"hh:mm\" disabled></th>");
		//	}

		//	sb.AppendLine("        </tr>");
		//	sb.AppendLine("    </tfoot>");
		//	sb.AppendLine("</table>");

		//	return sb.ToString();
		//}

		public class ForwardTimesheetModel
		{
			public string TimesheetHeaderUUID { get; set; }
			public string ProjectUUID { get; set; }
			public string TaskUUID { get; set; }
		}

		[HttpPost]
		public IActionResult ForwardTimesheet([FromBody] ForwardTimesheetModel model)
		{
			if (model == null || string.IsNullOrEmpty(model.TimesheetHeaderUUID) || string.IsNullOrEmpty(model.ProjectUUID) || string.IsNullOrEmpty(model.TaskUUID))
			{
				return BadRequest("Invalid data received.");
			}

			var currentHeader = s.TimesheetHeader.Get().FirstOrDefault(th => th.UUID == model.TimesheetHeaderUUID);
			if (currentHeader == null)
			{
				return NotFound("Timesheet Header not found.");
			}

			// Ensure From_Date has a value before calling AddDays(7)
			if (!currentHeader.From_Date.HasValue)
			{
				return BadRequest("Invalid From_Date in the current timesheet header.");
			}

			DateTime nextWeekStartDate = currentHeader.From_Date.Value.AddDays(7);

			// Check if next week's header already exists
			var nextWeekHeader = s.TimesheetHeader.Get()
				.FirstOrDefault(th => th.Emp_UUID == currentHeader.Emp_UUID && th.From_Date == nextWeekStartDate);

			// If it does not exist, create a new one
			if (nextWeekHeader == null)
			{
				nextWeekHeader = new Timesheet_Header
				{
					UUID = Guid.NewGuid().ToString(),
					Emp_UUID = currentHeader.Emp_UUID,
					From_Date = nextWeekStartDate,
					To_Date = currentHeader.To_Date?.AddDays(7), // Shift To_Date forward
					Timesheet_Status = "Pending"
				};
				s.TimesheetHeader.Add(nextWeekHeader);
			}

			// Get all timesheet lines that match the given criteria
			var timesheetLines = s.TimesheetLine.Get()
				.Where(tl => tl.TimesheetHeader_UUID == model.TimesheetHeaderUUID && tl.Project_UUID == model.ProjectUUID && tl.Task_UUID == model.TaskUUID)
				.ToList();

			if (!timesheetLines.Any())
			{
				return BadRequest("No valid timesheet lines found to forward.");
			}

			// Update each timesheet line individually
			foreach (var line in timesheetLines)
			{
				if (line.Date_of_Task.HasValue)
				{
					line.TimesheetHeader_UUID = nextWeekHeader.UUID; // Update Header UUID
					line.Date_of_Task = line.Date_of_Task.Value.AddDays(7); // Update date
					line.Day_of_Week = line.Date_of_Task.Value.DayOfWeek.ToString(); // Update day of the week

					// Assuming an Update function exists in your repository
					s.TimesheetLine.Update(line);
				}
			}

			return Ok(new { message = "Timesheet successfully forwarded!" });
		}




		[HttpPost]
		public IActionResult SaveTimesheetLine(string projectUUID, string taskUUID, string timesheetHeaderUUID)
		{
			if (string.IsNullOrEmpty(projectUUID) || string.IsNullOrEmpty(taskUUID) || string.IsNullOrEmpty(timesheetHeaderUUID))
			{
				return Json(new { success = false, message = "Invalid input data." });
			}

			string userUUID = Request.Cookies["UserUUID"]?.ToString();
			if (string.IsNullOrEmpty(userUUID))
			{
				return Json(new { success = false, message = "User not authenticated." });
			}

			// Ensure the Timesheet Header exists
			var header = s.TimesheetHeader.Get()
				.FirstOrDefault(c => c.UUID == timesheetHeaderUUID && c.IsActive == true);

			if (header == null)
			{
				return Json(new { success = false, message = "Timesheet Header not found." });
			}

			// Get start and end dates from Timesheet Header
			DateTime startDate = Convert.ToDateTime(header.From_Date);
			DateTime endDate = Convert.ToDateTime(header.To_Date);

			List<Timesheet_Line> timesheetLines = new List<Timesheet_Line>();

			// ✅ Generate a single UUID for the week's timesheet lines
			string commonUUID = u.GetUUID();

			// Create and save a new Timesheet Line entry for each day in the week
			for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
			{
				var newLine = new Timesheet_Line
				{
					UUID = commonUUID, // ✅ Using the common UUID for all entries in this week
					TimesheetHeader_UUID = timesheetHeaderUUID,
					Project_UUID = projectUUID,
					Task_UUID = taskUUID,
					Date_of_Task = date,
					Day_of_Week = date.DayOfWeek.ToString(),
					Hours = "00:00",
					Remark = "",
					IsActive = true,
					IsAddedOn = DateTime.Now,
					IsAddedBy = userUUID
				};

				s.TimesheetLine.Add(newLine);
				timesheetLines.Add(newLine);
			}



			return Json(new { success = true, timesheetLines });

		}




		public IActionResult ManageTimeSheetApproval()
        {
            return View();
        }
        public IActionResult ManageMyWorkList()
        {
            return View();
        }

    }
}
