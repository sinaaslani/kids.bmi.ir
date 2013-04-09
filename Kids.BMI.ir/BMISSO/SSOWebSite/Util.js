 /////////////////////////////Anthem Scripts////////////////////////////////////////////////
 function Anthem_PreCallBack()
  {	     
      
           
            var loading = document.createElement("div");
			loading.id = "loading";
			loading.style.fontFamily="Tahoma";
			loading.style.fontSize="12";
			loading.style.fontWeight.bold=false;
			loading.style.color = "black";
			loading.style.backgroundColor = "RED";
			loading.style.paddingLeft = "5px";
			loading.style.paddingRight = "5px";
			loading.style.position = "absolute";
			loading.style.right = "10px";
			loading.style.top = "10px";
			loading.style.zIndex = "9999";
			loading.innerHTML = "Loading...";
			document.body.appendChild(loading); 
      
	}
	function Anthem_CallBackCancelled() 
	{
		alert("درخواست شما لغو شد!");
		
	}
	function Anthem_PostCallBack() {
		var loading = document.getElementById("loading");
		document.body.removeChild(loading);
		
	}
	function Anthem_Error(result)
	{
	  
		  alert('Error In Application: \n' + 'خطا در سرور : لطفا جهت رفع مشكل با مديريت سايت تماس بگيريد'+result.error)
		  
	}