# **Task Management System (Full Stack)**

### **Overview**

> This Is A Full-Stack Task Management Application Built Using **Angular 19** For The Frontend And **ASP.NET Core Web API** For The Backend. The Goal Of This Project Was To Master The Data Flow Between A Modern SPA And A RESTful API While Maintaining Technical Precision In Both Architecture And UI.

### **Key Features**

- **Full CRUD Operations:** Users Can Create, Read, Update, And Delete Tasks.
- **Reactive Forms Integration:** Managed Data Entry Using **ReactiveForms** For Better Control And Validation.
- **Conditional UI Logic:** The "Mark As Completed" Button Disappears Automatically Once A Task Is Done.
- **Status Management:** Backend Validation Ensures A Completed Task Cannot Be Modified Or Re-Completed.
- **Modern Navigation:** Implemented A **Navbar** With Active Link Tracking For Smooth Navigation.

### **Mandatory Written Explanation**

#### **1\. How Does Data Flow From The Angular UI To The Backend?**

In My Project, The Data Flow Starts When The User Fills Out The **Reactive Form**. Once The Submit Button Is Clicked, The Component Collects The Form Values And Sends Them To A **Service**. This Service Uses **HttpClient** To Make An API Call (Like POST Or PATCH). The **ASP.NET Core Backend** Then Receives This Data In The Controller Through A **DTO**, Processes It, And Saves It To The Database Using **Entity Framework**.

#### **2\. How Does Dependency Injection Work In Your Backend?**

I Used The Built-In **Dependency Injection** In ASP.NET Core. I Registered My Repository In The Program.cs File Using builder.Services.AddScoped. Then, In My **TaskController**, I Requested The Repository Through The **Constructor**. This Way, The Framework Automatically Creates The Object For Me, Which Makes The Code Cleaner And Easier To Maintain.

#### **3\. How Do You Handle Validation And Errors?**

I Handled Validation In Two Places:

- **On The Frontend:** I Used Angular **Validators** (Like Validators.required) To Make Sure The User Enters Data Before The "Save" Button Is Enabled.
- **On The Backend:** I Added **Data Annotations** To My Model Properties. If The Data Is Missing, The API Automatically Returns A **400 BadRequest**. I Also Created A Custom GeneralErrorResponse Class To Make Sure All Error Messages From The API Have The Same Format.

#### **4\. Which Part Was Hardest For You And Why?**

The Hardest Part For Me Was The **Update Task** Feature. It Was Tricky To Get The Existing Data From The Backend And Fill It Into The **Reactive Form** Automatically (Data Hydration). I Had To Learn How To Use patchValue To Make Sure The Form Fields Were Correctly Populated As Soon As The Page Loaded.

#### **5\. If This App Had 10,000 Tasks, What Problems Might Occur And What Do You Suggest To Fix?**

If The App Had 10,000 Tasks, Loading Everything At Once Would Make The Page Very **Slow** And Use A Lot Of Memory. To Fix This, I Would Implement **Server-Side Pagination** On The Backend Using .Skip() And .Take() In LINQ. This Way, The API Only Sends 10 Or 20 Tasks At A Time. I Would Also Add A **Search Bar** In The UI So Users Can Find Specific Tasks Easily Without Scrolling Through The Whole List.

### **Tech Stack**

- **Frontend:** Angular 19, RxJS, Reactive Forms.
- **Backend:** .NET 10, ASP.NET Core Web API
- **Database:** In-Memory DB.

### **How To Run**

1.  **Backend:** Run dotnet run From The Backend Folder.
2.  **Frontend:** Run npm install Then ng serve From The Frontend Folder.
3.  **Access:** Open http://localhost:4200 In Your Browser.
