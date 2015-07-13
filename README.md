#Validation from Server for Angular Agility

Angular Agility form validation that is retrieved from Web API and DTO classes 
in the form of the [JSON object that AA expects](https://github.com/AngularAgility/AngularAgility/wiki/External-Form-Configuration).

[NuGet Package](https://www.nuget.org/packages/Intertech.Validation.AA/)

##Blog
[Angular Agility: AngularJS Validation Pulled from Web API](http://www.intertech.com/Blog/angular-agility-angularjs-validation-pulled-from-web-api/)

##Usage
Typically, the following code would reside on the Web API controller code:

```C#
	public IHttpActionResult GetValidations(string dtoObjectName, string jsonObjectName)
	{
		var valHelper = new ValidationHelper();
        object jsonObject = valHelper.GetValidations(dtoObjectName, jsonObjectName,
			"Namespace.If.Doesnt.Match.Assembly", useCamelCaseForProperties, "DTO.Assembly.Name");
	
	    return Ok(jsonObject);
	}
```
- dtoObjectName is the name of the class in your DTO assembly.
- jsonObjectName is the name of the model object on your website.
- "Namespace.If.Doesnt.Match.Assembly" is an alternate namespace...by default is uses DTO.Assembly.Name.
- "useCamelCaseForProperties" is a bool indicating if the helper should provide case alteration to property names to produce camel case.
- "DTO.Assembly.Name" is the name of the assembly to load for reflecting the DTO properties that have validation attributes.

##Using GetValidationsParms to call GetValidations
If your project is using a separate assembly for resource files, use the GetValidationsParms version of GetValidations.
The call would now look like this:

```C#
	public IHttpActionResult GetValidations(string dtoObjectName, string jsonObjectName)
	{
		var valHelper = new ValidationHelper();
		var parms = new GetValidationsParms(dtoObjectName, jsonObjectName);
		parms.DtoAlternateNamespace = "Namespace.If.Doesnt.Match.Assembly";
		parms.DtoAssemblyNames = "DTO.Assembly.Name";
		parms.UseCamelCaseForProperties = true;
		parms.ResourceNamespace = "Namespace.For.Resource";
		parms.ResourceAssemblyName = "Resource.Assembly.Name";

        object jsonObject = valHelper.GetValidations(parms);
	
	    return Ok(jsonObject);
	}
```


##DTO Example
```C#
    namespace AATestAPI.Models
    {
    public class ValidationTest
    {
        [Required]
        public string Name { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Url]
        public string Website { get; set; }

        [CreditCard]
        public string CreditCard { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(5)]
        public string MaxLengthTest { get; set; }

        [MinLength(2)]
        public string MinLengthTest { get; set; }

        [Range(1, 5)]
        public int RangeTest { get; set; }

        [RegularExpression(RegexConstants.Integer, ErrorMessage = "Must be an integer")]
        public string IntegerString { get; set; }

        [StringLength(10, MinimumLength = 2)]
        public string StringLengthTest { get; set; }
    }
	}
```
##AngularJS Website
###View
This is a typical Angular Agility form:

    <div class="page-header">
        <h1>Angular Agility Validation From Server</h1>
    </div>
    <div aa-configured-form validation-config="formconfig" class="form-horizontal" ng-form="aatest">
        <input aa-field-group="model.Name" />
        <input aa-field-group="model.Phone" />
        <input aa-field-group="model.Website" />
        <input aa-field-group="model.CreditCard" />
        <input aa-field-group="model.Email" />
        <input aa-field-group="model.MaxLengthTest" />
        <input aa-field-group="model.MinLengthTest" />
        <input aa-field-group="model.RangeTest" type="number" />
        <input aa-field-group="model.IntegerString" />
        <input aa-field-group="model.StringLengthTest" />
    
        <button aa-submit-form="submit()" type="submit" class="btn btn-primary">Submit</button>
     </div>

###UI-ROUTER State Definition
Use the 'resolve' property and return a promise so that the controller doesn't get created until the validation is retrieved from the server.
See this excellent explanation of this technique: [How to force AngularJS resource resolution with ui-router](http://www.jvandemo.com/how-to-resolve-angularjs-resources-with-ui-router/).
Of course this assumes you are using [ui-router](https://github.com/angular-ui/ui-router).

```javascript
    angular.module('app', [
        'ui.router',
        'ui.bootstrap',
        'aa.formExtensions',
        'aa.formExternalConfiguration',
        'aa.notify',
        'aa.select2'
    ])
    .config(['$stateProvider', function ($stateProvider) {
        $stateProvider.state('aatest', {
            url: ''
            , resolve: {
                validationService: 'validationService',
                validationData: function (validationService) {
                    var valData = validationService.getValidations('ValidationTest', 'model');
                    return valData;
                }
            }
            , views: {
                'content@': {
                    templateUrl: 'aatest.html'
                    , controller: 'aatestController'
                }
            }
        });
    }]);
```
###Controller
The controller now can inject validationData since it was resolved in the router definition.

```javascript
    angular
        .module('app')
        .controller('aatestController', aatestController);

    aatestController.$inject = ['$scope', 'validationData'];

    function aatestController($scope, validationData) {
        activate();

        function activate() {
            $scope.model = {};
            $scope.formconfig = angular.isUndefined(validationData) ? undefined : validationData.data;
        }
    }
```

## Contributing

See file [development.md](development.md) for more infos on the project structure.
