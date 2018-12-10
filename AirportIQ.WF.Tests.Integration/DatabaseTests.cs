using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirportIQ.WF;
using AirportIQ.WF.Config;
using AirportIQ.WF.Steps;
using System.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Data.SqlServerCe;

namespace AirportIQ.Tests.WF.Integration
{
    [TestClass]
    public class DatabaseTests
    {
        private static WorkflowService _workflowService;
        private const string AssignToUser = "95"; // 95 = tmeyer

        [ClassInitialize]
        public static void TestSetup(TestContext context)
        {
            _workflowService = new WorkflowService()
                                .WithSMTPInfo(new SMTPInfo
                                {
                                    Host = "some server",
                                    Port = "8080",
                                    Username = "username",
                                    Password = "password"
                                })
                                //.WithDatabaseInfo(new DatabaseInfo
                                //{
                                //    Connection = new SqlCeConnection
                                //    {
                                //        ConnectionString = @"Data Source=Database.Tests.sdf"
                                //    }
                                //})
                                .WithDatabaseInfo(new DatabaseInfo
                                {
                                    Connection = new SqlConnection
                                    {
                                        ConnectionString = @"data source=SQL08-ATC-01.GCR1.COM;User ID=AirportIQ Secure Credentialing -- SBO;Password=fubar;Initial Catalog=CS_BOSD -- Dev;Network Library=dbmssocn;"
                                    },
                                    Schema = "Data"
                                })
                                .AddCustomWorkflowType(typeof(PayraiseWorkflow))
                                .Start();
        }


        [TestMethod]
        public void Can_save_workflow_to_database()
        {
            // Arrange
            PayraiseWorkflow workflow = new PayraiseWorkflow
            {
                Employee = "Bob Dole",
                RequestedSalaryAmount = 30000
            };

            WorkflowStep step1 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };
            WorkflowStep step2 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };
            WorkflowStep step3 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };

            workflow.AddStep(step1);
            workflow.AddStep(step2);
            workflow.AddStep(step3);

            // Act

            // Adding a workflow saves it to the database.
            _workflowService.AddWorkflow(workflow);
        }

        [TestMethod]
        public void Can_retrieve_workflow_from_database()
        {
            // Arrange
            PayraiseWorkflow workflow = new PayraiseWorkflow
            {
                Employee = "Bob Dole",
                RequestedSalaryAmount = 30000
            };

            WorkflowStep step1 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };
            WorkflowStep step2 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };
            WorkflowStep step3 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };

            workflow.AddStep(step1);
            workflow.AddStep(step2);
            workflow.AddStep(step3);

            // Act

            // Adding a workflow saves it to the database.
            _workflowService.AddWorkflow(workflow);

            _workflowService.LoadWorkflows();
        }

        [TestMethod]
        public void Can_save_and_reload_multiple_workflows()
        {
            // Arrange
            PayraiseWorkflow workflow1 = new PayraiseWorkflow
            {
                Employee = "Bob Dole",
                RequestedSalaryAmount = 30000
            };

            WorkflowStep step1 = new WorkflowStep { Title = "Request for payraise: " + workflow1.Employee, AssignedTo = AssignToUser };

            workflow1.AddStep(step1);

            TerminationWorkflow workflow2 = new TerminationWorkflow
            {
                Employee = "Burt Reynolds",
                TerminationReason = "Too much mustache"
            };

            WorkflowStep step2 = new WorkflowStep { Title = "Termination: " + workflow2.Employee, AssignedTo = AssignToUser };

            workflow2.AddStep(step2);

            // Act

            // Adding a workflow saves it to the database.
            _workflowService.AddWorkflow(workflow1);
            _workflowService.AddWorkflow(workflow2);

            _workflowService.LoadWorkflows();
        }

        [TestMethod]
        public void Can_retrieve_specific_workflow()
        {
            // Arrange
            PayraiseWorkflow workflow1 = new PayraiseWorkflow
            {
                Employee = "Bob Dole",
                RequestedSalaryAmount = 30000
            };

            WorkflowStep step1 = new WorkflowStep { Title = "Request for payraise: " + workflow1.Employee, AssignedTo = AssignToUser };

            workflow1.AddStep(step1);

            TerminationWorkflow workflow2 = new TerminationWorkflow
            {
                Employee = "Burt Reynolds",
                TerminationReason = "Too much mustache"
            };

            WorkflowStep step2 = new WorkflowStep { Title = "Termination: " + workflow2.Employee, AssignedTo = AssignToUser };

            workflow2.AddStep(step2);

            // Act

            // Adding a workflow saves it to the database.
            _workflowService.AddWorkflow(workflow1);
            _workflowService.AddWorkflow(workflow2);

            var id = workflow2.Id;

            _workflowService.LoadWorkflows();

            var retrievedWorkflow = _workflowService.Workflows.Where(w => w.Key == id).Select(w => w.Value).FirstOrDefault();

            // Assert
            Assert.IsNotNull(retrievedWorkflow);
        }

        [TestMethod]
        public void Custom_workflow_types_are_retrieved_correctly()
        {
            // Arrange
            TerminationWorkflow workflow = new TerminationWorkflow
            {
                Employee = "Burt Reynolds",
                TerminationReason = "Too much mustache"
            };

            WorkflowStep step2 = new WorkflowStep { Title = "Termination: " + workflow.Employee, AssignedTo = AssignToUser };

            workflow.AddStep(step2);

            // Act

            // Adding a workflow saves it to the database.
            _workflowService.AddWorkflow(workflow);

            var id = workflow.Id;

            _workflowService.LoadWorkflows();

            var retrievedWorkflow = _workflowService.Workflows.Where(w => w.Key == id).Select(w => w.Value).FirstOrDefault();

            // Assert
            Assert.AreEqual(typeof(TerminationWorkflow).ToString(), retrievedWorkflow.GetType().ToString());
            Assert.AreEqual("Too much mustache", ((TerminationWorkflow)retrievedWorkflow).TerminationReason);
        }

        [TestMethod]
        public void Can_save_and_retrieve_notes()
        {
            // Arrange
            PayraiseWorkflow workflow = new PayraiseWorkflow
            {
                Employee = "Bob Dole",
                RequestedSalaryAmount = 30000
            };

            // Act
            WorkflowStep step1 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };
            WorkflowStep step2 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };
            WorkflowStep step3 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };

            Note note = new Note { CreatedBy = "ldunnehoo", Text = "blah blah blah" };
            step1.AddNote(note);

            note = new Note { CreatedBy = "burt reynolds", Text = "fedfdsfdf" };
            step2.AddNote(note);

            note = new Note { CreatedBy = "bob dole", Text = "upoo98i 9o698" };
            step2.AddNote(note);

            workflow.AddStep(step1);
            workflow.AddStep(step2);
            workflow.AddStep(step3);        

            // Adding a workflow saves it to the database.
            _workflowService.AddWorkflow(workflow);

            Guid id = workflow.Id;

            _workflowService.LoadWorkflows();

            var retrievedWorkflow = _workflowService.Workflows.Where(w => w.Key == id).Select(w => w.Value).FirstOrDefault();

            // Assert
            Assert.AreEqual(3, retrievedWorkflow.Notes.Count);
        }

        [TestMethod]
        public void Can_save_and_retrieve_attachments()
        {
            // Arrange
            PayraiseWorkflow workflow = new PayraiseWorkflow
            {
                Employee = "Bob Dole",
                RequestedSalaryAmount = 30000
            };

            // Act
            WorkflowStep step1 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };
            WorkflowStep step2 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };
            WorkflowStep step3 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };

            string executingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filename = "test.txt";
            byte[] fileArray = File.ReadAllBytes(Path.Combine(executingDirectory, filename));

            Attachment attachment = new Attachment { CreatedBy = "ldunnehoo", Filename = filename, File = fileArray };
            step1.AddAttachment(attachment);

            attachment = new Attachment { CreatedBy = "burt reynolds", Filename = filename, File = fileArray };
            step2.AddAttachment(attachment);

            attachment = new Attachment { CreatedBy = "bob dole", Filename = filename, File = fileArray };
            step2.AddAttachment(attachment);

            attachment = new Attachment { CreatedBy = "bob dole", Filename = filename, File = fileArray };
            step3.AddAttachment(attachment);

            workflow.AddStep(step1);
            workflow.AddStep(step2);
            workflow.AddStep(step3);

            // Adding a workflow saves it to the database.
            _workflowService.AddWorkflow(workflow);

            Guid id = workflow.Id;

            _workflowService.LoadWorkflows();

            var retrievedWorkflow = _workflowService.Workflows.Where(w => w.Key == id).Select(w => w.Value).FirstOrDefault();

            // Assert
            Assert.AreEqual(4, retrievedWorkflow.Attachments.Count);
        }

        [TestMethod]
        public void Saving_a_workitem_doesnt_create_duplicates()
        {
            // Arrange
            PayraiseWorkflow workflow = new PayraiseWorkflow
            {
                Employee = "Bob Dole",
                RequestedSalaryAmount = 30000
            };

            WorkflowStep step1 = new WorkflowStep { Title = "Request for payraise: " + workflow.Employee, AssignedTo = AssignToUser };

            workflow.AddStep(step1);

            // Act

            // Adding a workflow saves it to the database.
            _workflowService.AddWorkflow(workflow);

            workflow.RequestedSalaryAmount = 35000;

            workflow.Save();
        }
    }
}
