#Validation from Server for Angular Agility

Angular Agility form validation that is retrieved from Web API and DTO classes 
in the form of the [JSON object that AA expects](https://github.com/AngularAgility/AngularAgility/wiki/External-Form-Configuration).

[NuGet Package](https://www.nuget.org/packages/Intertech.Validation.AA/)

##Usage
Typically, the following code would reside on the Web API controller code:

	public IHttpActionResult GetValidations(string dtoObjectName, string jsonObjectName)
	{
		var valHelper = new ValidationHelper();
        object jsonObject = valHelper.GetValidations(dtoObjectName, jsonObjectName,
			"Namespace.If.Doesnt.Match.Assembly", "DTO.Assembly.Name");
	
	    return Ok(jsonObject);
	}

- dtoObjectName is the name of the class in your DTO assembly.
- jsonObjectName is the name of the model object on your website.
- "Namespace.If.Doesnt.Match.Assembly" is an alternate namespace...by default is uses DTO.Assembly.Name.
- "DTO.Assembly.Name" is the name of the assembly to load for reflecting the DTO properties that have validation attributes.