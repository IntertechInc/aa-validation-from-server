#Validation from Server for Angular Agility

Angular Agility form validation that is retrieved from Web API and DTO classes 
in the form of the [JSON object that AA expects](https://github.com/AngularAgility/AngularAgility/wiki/External-Form-Configuration).

[NuGet Package](https://www.nuget.org/packages/Intertech.Validation.AA/)

##Usage
Typically, the following code would reside on the Web API controller code:

	public IHttpActionResult GetValidations(string dtoObjectName, string jsonObjectName)
	{
		var valHelper = new ValidationHelper();
        object jsonObject = valHelper.GetValidations(dtoObjectName, jsonObjectName, "Namespace.If.Doesnt.Match.Assembly", "DTO.Assembly.Name");
	
	    return Ok(jsonObject);
	}