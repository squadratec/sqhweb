﻿$.blockUI.defaults.css.backgroundColor = 'transparent'; 
$.blockUI.defaults.css.border = 'none'; 
$.blockUI.defaults.message = '<img src="/images/eclipse.gif" />';

$(document).ajaxStart($.blockUI).ajaxStop($.unblockUI);