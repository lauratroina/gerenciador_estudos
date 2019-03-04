"use strict";
var HelpJS = {
	Array: {
		select: function (array, action) {
			var result = [];
			for (var i = 0, length = array.length; i < length; i++) {
				result.push(action(array[i]));
			}
			return result;
		},
		where: function (array, action) {
			var result = [];
			for (var i = 0, length = array.length; i < length; i++) {
				if(action(array[i])){
					result.push(array[i]);
				}
			}
			return result;
		},
		sum: function (array, action) {
			var sum = 0;
			for (var i = 0, length = array.length; i < length; i++) {
				sum += action(array[i]);
			}
			return sum;
		},
		clear: function (array) {
			while (array.length) {
				array.pop();
			}
		},
		limit: function (array, limit) {
			var result = [];
			for (var i = 0, length = array.length; i < limit && i < length; i++) {
				result.push(array[i]);
			}
			return result;
		},
		keys: function (array) {
			result = [];
			for (var key in Object.keys(array)) {
				if (arr.hasOwnProperty(key)) {
					result.push(array[i]);
				}
			}
			return result;
		}
	},
	Number: {
		pad: function (str, max) {
			str = str.toString();
			return str.length < max ? HelpJS.Number.pad("0" + str, max) : str;
		}
	},
	String: {
		replaceAll: function (str, find, replace) {
			return str.replace(new RegExp(find.replace(/[-\/\\^$*+?.()|[\]{}]/g, '\\$&'), 'g'), replace);
		}
	},
	Http: {
		Cookie: {
			set: function(key,value,life){
				var d = new Date();
				d.setTime(d.getTime() + (life*86400000));
				var expires = "expires="+d.toGMTString();
				document.cookie = key + "=" + value + "; " + expires;
			},
			get: function(key){
				var cookies = document.cookie.split(";");
				for(var i = cookies.length-1; i > -1; i--) {
					var cookieData = cookies[i].replace(" ","").split("=");
					if(cookieData[0] === key) {
						return cookieData[1];
					}
				}
				return null;
			},
			remove: function(key){
				var d = new Date();
				d.setTime(d.getTime() + -1);
				var expires = "expires="+d.toGMTString();
				document.cookie = key + "=''; " + expires;
			},
			has: function(key){
				rgx = new RegExp(/;?[ ]?([a-z]+)=/gi);
				while((array = rgx.exec(document.cookie)) != null) {
					if(array[1] === key)
						return true;
				}
				return false;
			},
		},
		methods: {
			OPTIONS: "OPTIONS",
			GET: "GET",
			HEAD: "HEAD",
			POST: "POST",
			PUT: "PUT",
			DELETE: "DELETE",
			TRACE: "TRACE",
			CONNECT: "CONNECT"
		},
		parametersToObject: function(url){
			var result = {};
			var splited = url.split("#")[0].split("?")[1].split("&");
			for(var propertie in splited){
				var propertie = splited[propertie];
				result[propertie.split("=")[0]] = unescape(propertie.split("=")[1]);
			}
			return result;
		},
		objectToParameters: function(object){
			var result = [];
			for(var propertie in object){
				result.push(propertie + "=" + object[propertie]);
			}
			return "?"+result.join("&");
		},
		send: function(request,sucessCallback,errorCallback){
			request = {
				parameters: (request.parameters?HelpJS.Http.objectToParameters(request.parameters):""),
				url: request.url,
				method: request.method||"GET",
				async: request.async||true,
				user: request.user||"",
				password: request.password||"",
				data: request.data,
				headers: request.headers||{}
			};
			var xmlhttp = new XMLHttpRequest();
			xmlhttp.onreadystatechange=function(){
				if (xmlhttp.readyState==4){
					if(xmlhttp.status>=200 && xmlhttp.status<300){
						sucessCallback(xmlhttp.responseText, xmlhttp.status);
					}else {
						errorCallback(xmlhttp.responseText, xmlhttp.status);
					}
				}
			};
			xmlhttp.open(request.method, request.url + request.parameters, request.async, request.user, request.password);
			for(var propertie in request.headers){
				xmlhttp.setRequestHeader(propertie, request.headers[propertie]);
			}
			(request.data)?xmlhttp.send(request.data):xmlhttp.send();
		}
	}
};
