// Common pin Js
$.extend(true, pinJs, {
    init: function () {
        var t = this;

        //init
        $(document).ready(function () {
            //ActiveMenu.init();
        });                

        $(".a-link-action").click(function (e) {
            e.preventDefault();
            var link = $(this).attr("url");
            if (!link.match(/^http([s]?):\/\/.*/)) {
                link = 'http://' + link;
            }
            window.open(link, '_blank');
        });

        //Handler for document
        $(document)
            //Modal close alert
            .on("click", "div.noty_modal", function () {
                //$.noty.closeAll();
            })
            //Modal Ajax for body - http://api.jquery.com/category/ajax/global-ajax-event-handlers/
            .on({
                // When ajaxStart is fired, add 'loading' to body class
                ajaxStart: function (event) {
                    if ($("#loading-ajax").length == 0)
                        $("#loading-ajax").removeClass("hide");
                },
                // When ajaxStop is fired, rmeove 'loading' from body class
                ajaxStop: function () {
                    $("#loading-ajax").addClass("hide");
                    //$.scroll.resize();
                },
                ajaxError: function (event, request, settings) {
                    $("#loading-ajax").addClass("hide");
                    //your error handling code here
                },
                ajaxSend: function (event, request, settings) {
                    var urls = [];
                    urls.push("/State/WindowState");
                    $.each(urls, function (k, url) {
                        if (settings.url.substr(0, url.length) == url) {
                            $("#loading-ajax").addClass("hide");
                            return false;
                        }
                    });

                },
                ajaxComplete: function (event, request, settings) {

                }
            }).on("click", ".comingsoon", function (e) {
                e.preventDefault();
                t.msgWarning({ text: "Coming soon" });
            });

        $.ajaxSetup({
            statusCode: {
                401: function () {
                    t.msgWarning({
                        text: t.l('Common_Sessiontimeout'),
                        callback: {
                            onClose: function () {
                                window.location.href = $('.url-common').attr('data-url-login');
                            }
                        }
                    });
                }
            },
            // Disable caching of AJAX responses
            cache: false
        });

        return this;
    },
    initController: function (param) {
        var parts = param.split(".");
        for (var i = 0, len = parts.length, obj = window; i < len; ++i) {
            obj = obj[parts[i]];
        }
        if (typeof (obj) == 'object' && obj.hasOwnProperty('init')) {
            obj.init();
        }
    },

    globalAjax: function (options) {
        pinJs.checkTimeOut();
        $.ajax({
            url: options.url,
            type: "POST",
            dataType: "json",
            //async: false,
            data: options.data,
            success: function (response) {
                if (response.Status == 1) {
                    options.callbackSuccess(response);
                } else {
                    options.callbackError(response);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    },

    checkTimeOut: function () {        
        var urlCheck = location.protocol + "//" + location.hostname + (window.location.port ? ":" + window.location.port : "") + "/Dashboard/CheckAuthorize";
        var urlRedirect = location.protocol + "//" + location.hostname + (window.location.port ? ":" + window.location.port : "") + "/Home";
        $.post(urlCheck, {}, function (data) {
            if (data == "1") {
                //pass
            } else {
                //redirect: timeout
                window.location.replace(urlRedirect);
                return false;
            }
        });        
    },

    redirectToUrl: function (newUrl) {
        var urlRedirect = location.protocol + "//" + location.hostname + (window.location.port ? ":" + window.location.port : "") + newUrl;
        window.location.replace(urlRedirect);
        return false;
    },

    openPopupWarningMID: function () {
        var options = {
            id: 'popupWarningMID',
            width: 450,
            title: false,
            draggable: false,
            resizable: false
        }
        var popupWindow = window.pinJs.initPopupWindow(options);
        popupWindow.refresh({
            url: "/Payment/PopupWarningMID",
            async: false
        }).center().open();
        $('#popupWarningMID').parent().addClass('popup-window');
        $("#popupWarningMID").parent().addClass("mobileModal");
        $('#popupWarningMID').find(".popup-payment-mid-close").on("click", function () {
            if (popupWindow) popupWindow.close();
        });
    },
    openPopupWarningMIDDashboard: function () {
        var options = {
            id: 'popupWarningMID',
            width: 450,
            title: false,
            draggable: false,
            resizable: false
        }
        var popupWindow = window.pinJs.initPopupWindow(options);
        popupWindow.refresh({
            url: "/Payment/PopupWarningMIDDashboard",
            async: false
        }).center().open();
        $('#popupWarningMID').parent().addClass('popup-window');
        $("#popupWarningMID").parent().addClass("mobileModal");
        $('#popupWarningMID').find(".popup-payment-mid-close").on("click", function () {
            if (popupWindow) popupWindow.close();
        });
    },

    initPopupWindow: function (options) {
        pinJs.checkTimeOut();
        var defaultOptions = {
            id: 'popup',
            minWidth: 100,
            minHeight: 100,
            width: 600,
            modal: true,
            draggable: false,
            resizable: false,
            title: 'Popup title'
        };
        options = $.extend(defaultOptions, options);
        var popup = $('#' + options.id);
        if (!popup.length) {
            $('body').append('<div id="' + options.id + '"></div>');
            popup = $('#' + options.id);
        }
        var popupWindow = popup.data('kendoWindow');
        if (!popupWindow) {
            popup.kendoWindow({
                minWidth: options.minWidth,
                minHeight: options.minHeight,
                width: options.width,
                title: options.title,
                modal: options.modal,
                resizable: options.resizable,
                draggable: options.draggable
            });
            popupWindow = popup.data('kendoWindow');
        } else {
            popupWindow.title(options.title);
        }
        return popupWindow;
    },
    initFormWindow: function (formId, popupWindow, grid, callback) {        
        var $form = $(formId);
        //handler button
        $form.find(".k-grid-cancel").on("click", function () {
            if (popupWindow) {
                popupWindow.close();
                $form.remove();
            }
        });        
        $(".content-modal .modal-header").find(".close").on("click", function () {
            if (popupWindow) {
                popupWindow.close();
                $form.remove();
            }
        });        

        $form.find(".k-grid-update").on("click", function () {            
            $form.submit();
        });
        //handler post form
        $.validator.unobtrusive.parse($form);
        var validator = $form.data("validator");
        validator.settings.ignore = ':hidden, .ignore, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                    $(formId + " .k-grid-update").attr("disabled", "disabled");
                },
                success: function (result, textStatus, jqXHR) {
                    if (result.Success == true) {                        
                        if (popupWindow) {
                            popupWindow.close();
                            $(form).remove();
                        }
                        if (callback && typeof (callback) == 'function') {
                            callback(result);
                        } else {
                            if (grid) {
                                alertify.success(result.Message);
                                var gridData = grid.data("kendoGrid");
                                if (gridData) gridData.dataSource.read();
                            }                            
                        }
                    } else if (result.Success == 'failed') {
                        if (callback && typeof (callback) == 'function') {
                            callback(result);
                        }
                    } else {
                        var error = {}, errorNoKey = {};
                        if (result.Errors) {
                            $.each(result.Errors, function (k, m) {
                                if (k !== '') {
                                    error[k] = m;
                                } else {
                                    errorNoKey[k] = m;
                                }
                            });
                        }
                        validator.showErrors(error);
                        var container = $form.find(".validate-summary-nokey");
                        container.empty();
                        container.removeClass('hidden');
                        $.each(errorNoKey, function (k, m) {
                            container.append(m);
                        });
                    }

                    $(form).find(".k-grid-update").removeAttr('disabled');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //if fails
                    $(form).find(".k-grid-update").removeAttr('disabled');
                }
            });
        };
        //focus first text box
        pinJs.focusFirstInputForm($form);
    },
    initFormWindowWithConfirm: function (formId, popupWindow, textConfirm, callback) {
        var $form = $(formId);
        //handler button
        $form.find(".k-grid-cancel").on("click", function () {
            if (popupWindow) popupWindow.close();
        });
        //ThangND [2016-10-11] [TZ-2880: Update Member Contacts - School/Teacher can send an email and SMS notification]
        $(".content-modal .modal-header").find(".close").on("click", function () {
            if (popupWindow) popupWindow.close();
        });
        //End

        //fix close for modal
        $(".content-modal").find(".k-grid-cancel").on("click", function () {
            if (popupWindow) popupWindow.close();
        });

        $form.find(".k-grid-update").on("click", function () {
            window.pinJs.msgConfirm({
                text: textConfirm,
                buttons: [
                    {
                        text: 'Yes',
                        onClick: function ($noty) {
                            $noty.close();
                            $form.submit();
                        }
                    },
                    {
                        text: 'No',
                        onClick: function ($noty) {
                            $noty.close();
                        }
                    }
                ]
            });
        });
        //handler post form
        $.validator.unobtrusive.parse($form);
        var validator = $form.data("validator");
        validator.settings.ignore = ':hidden, .ignore, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                    $(formId + " .k-grid-update").attr("disabled", "disabled");
                    pinJs.blockUIForLoading();
                },
                success: function (result, textStatus, jqXHR) {
                    if (result.Success == true) {
                        if (result.ContentAddLesson != undefined && result.ContentAddLesson != "") {
                            if (callback && typeof (callback) == 'function') {
                                callback(result);
                            } else {
                                if (grid) {
                                    var gridData = grid.data("kendoGrid");
                                    if (gridData) gridData.dataSource.read();
                                }
                                //location.reload();
                            }
                        } else {
                            if (popupWindow) {
                                popupWindow.close();
                            }
                            if (callback && typeof (callback) == 'function') {
                                callback(result);
                            } else {
                                if (grid) {
                                    var gridData = grid.data("kendoGrid");
                                    if (gridData) gridData.dataSource.read();
                                }
                                //location.reload();
                            }
                        }

                    } else if (result.Success == 'failed') {
                        if (callback && typeof (callback) == 'function') {
                            callback(result);
                        }
                    } else {
                        validator.showErrors(result.Errors);
                    }

                    $(form).find(".k-grid-update").removeAttr('disabled');
                    pinJs.unBlockUIForLoading();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //if fails
                    $(form).find(".k-grid-update").removeAttr('disabled');
                    pinJs.unBlockUIForLoading();
                    alertify.error('Please choose Plan!');
                }
            });
        };
        //focus first text box
        pinJs.focusFirstInputForm($form);
    },
    initFormWithoutPost: function (formId, popupWindow) {
        var $form = $(formId);
        //handler button
        $form.find(".k-grid-cancel").on("click", function () {
            if (popupWindow) popupWindow.close();
        });

        //fix close for modal
        $(".content-modal").find(".k-grid-cancel").on("click", function () {
            if (popupWindow) popupWindow.close();
        });

        $form.find(".k-grid-update").on("click", function () {
            $form.submit();
        });
    },
    recurringReject: function (GuiId) {
        $.ajax(
            {
                url: '/ProfileInformation/RecurringPlanReject/' + GuiId,
                type: "POST",
                data: {
                    id: GuiId
                },
                beforeSend: function (xhr, opts) {
                    $("#btn-plan-register").attr("disabled", "disabled");
                    $("#btn-plan-reject").attr("disabled", "disabled");
                },
                success: function (data) {
                    if (data.Success == 1) {
                        alertify.success(data.Message);
                        window.location.href = "/";
                    } else {
                        alertify.error(data.Message);
                        $("#btn-plan-register").removeAttr("disabled");
                        $("#btn-plan-reject").removeAttr("disabled");
                    }
                }
            });
    },
    getTimeZone: function () {
        var offset = new Date().getTimezoneOffset(), o = Math.abs(offset);
        return offset;
    },
    recurringAccept: function (GuiId, _tokenPayment, _date, _timeStart, _timeEnd, _offset) {
        $.ajax(
            {
                url: '/ProfileInformation/RecurringPlanAccept/' + GuiId,
                type: "POST",
                data: {
                    id: GuiId,
                    tokenPayment: _tokenPayment,
                    date: _date,
                    timeStart: _timeStart,
                    timeEnd: _timeEnd,
                    offsetClient: _offset
                },
                beforeSend: function (xhr, opts) {
                    $("#btn-plan-register").attr("disabled", "disabled");
                    $("#btn-plan-reject").attr("disabled", "disabled");
                },
                success: function (data) {
                    if (data.Success == 1) {
                        alertify.success(data.Message);
                        //$("#message").val("Register successfull");
                        $(".group-action-plan-recurring").hide();
                        var starttime = $(".page-recurring-plan #StartTime");
                        if (starttime) {
                            starttime.attr("disabled", "disabled");
                        }
                        $(".page-recurring-plan .schedule-start-date").attr("disabled", "disabled");
                        $(".page-recurring-plan .schedule-start-date").parent().addClass("save-done");
                        $(".page-recurring-plan #recurring-plan-card-token").attr("disabled", "disabled");
                        $(".page-recurring-plan #recurring-plan-card-token").addClass("save-done");
                        $(".page-recurring-plan .group-required").hide();

                        //window.location.href = "/";
                        $(".goup-plan-msg-successfull").show();
                    } else {
                        alertify.error(data.Message);
                        $("#btn-plan-register").removeAttr("disabled");
                        $("#btn-plan-reject").removeAttr("disabled");
                    }
                }
            });
    },
    initFormPlantPost: function (infoForm, zone, isEdit, isProfile) {
        //handler post form
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                    $(zone + " .btn-save-card-builder").attr("disabled", "disabled");
                },
                success: function (data) {
                    if (data.Success == 1) {
                        $(zone + " .group-plans").removeClass("dsp-none");
                        $(zone + " .group-plans").show();
                        //ContentHTML
                        $(zone + " .region-plans").html(data.ContentHTML);

                        if (isEdit) {
                            zone = "#page-card-builder";
                            $(zone + " .region-plans").html(data.ContentHTML);
                            var options = {
                                id: "popPlanInfo",
                                width: 700,
                                title: false,
                                draggable: false,
                                resizable: false
                            };
                            var popupWindow = pinJs.initPopupWindow(options);
                            if (popupWindow) popupWindow.close();
                            pinJs.actionPlan(zone);
                        } else {
                            if (isProfile) {
                                pinJs.actionPlan(zone, true, zone + " #frmCardBuilder");
                            } else {
                                pinJs.actionPlan(zone, false, infoForm);
                            }
                            $(zone + " .box-update-plan").hide();

                        }
                        //re-init action                        
                        infoForm.trigger("reset");
                        pinJs.setRadioCheck(zone + " #radRecurringBilling", true);
                        pinJs.setRadioCheck(zone + " #radOnTimePayment", false);
                        $(zone + " #msg-valid-recurring").html("");
                        $(zone + " #IsRecurringBilling").val(true);
                        infoForm.find("#img-preview-thumbnail").addClass("dsp-none");
                        //pinJs.resetFormPlan(zone + " #frmCardBuilder");
                        //sunv addded 2016-10-11 
                        $(zone + " #ListPaymentMethodID").val("");
                        $(zone + " #payment-method-name").html("TZ Pay");
                        $(zone + " #ProratedFee").val("");
                        $(zone + " #RecurringMethodID").val(1);

                        if ($('#PromoCode').val() === "") {
                            $("#PromotionalDiscountPercentage").addClass('disable');
                            $("#PromotionalDiscountCash").addClass('disable');
                            $("#radpromoCodeExpireNoEndDate").attr('disabled', true);
                            $("#radpromoCodeExpireAfterDate").attr('disabled', true);
                            $('.promoCodeEndDate').addClass('disable');
                        }
                        alertify.success(data.Message);
                        $(zone + " .row-number-of-lesson").hide();
                    } else {
                        alertify.error(data.Message);
                        if (data.Errors.NumberPerPackage) {
                            $(zone + " #NumberPerPackage").removeClass("valid").addClass("input-validation-error");
                            $(zone + " span[data-valmsg-for=NumberPerPackage]").html(data.Errors.NumberPerPackage);
                            $(zone + " span[data-valmsg-for=NumberPerPackage]").removeClass("field-validation-valid").addClass("field-validation-error");
                        }
                    }
                    $(zone + " .btn-save-card-builder").removeAttr("disabled", "disabled");
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    $(zone + " .btn-save-card-builder").removeAttr("disabled", "disabled");
                }
            });
        }
    },
    clearErrorNumberPerPackage: function (zone) {
        $(zone + " #NumberPerPackage").removeClass("input-validation-error").addClass("valid");
        $(zone + " span[data-valmsg-for=NumberPerPackage]").html('');
        $(zone + " span[data-valmsg-for=NumberPerPackage]").removeClass("field-validation-error").addClass("field-validation-valid");
    },
    actionPlan: function (zone, isProfile, formId) {
        $(zone + " .box-list-plan .plan-edit").off();
        $(zone + " .box-list-plan .plan-edit").on("click", function () {
            if (isProfile) {
                $(zone + " .box-update-plan").show();
                pinJs.editPlan($(this).attr("plan-id"), isProfile, formId, zone);
            } else {
                pinJs.editPlan($(this).attr("plan-id"));
            }
        });
        $(zone + " .item-plan .plan-edit").off();
        $(zone + " .item-plan .plan-edit").on("click", function () {
            if (isProfile) {
                $(zone + " .box-update-plan").show();
                pinJs.editPlan($(this).attr("plan-id"), isProfile, formId, zone);
            } else {
                pinJs.editPlan($(this).attr("plan-id"));
            }
        });
        $(zone + " .box-list-plan .plan-del").off();
        $(zone + " .box-list-plan .plan-del").on("click", function () {
            if (isProfile) {
                pinJs.delPlan($(this).attr("plan-id"), zone, true);
            } else {
                pinJs.delPlan($(this).attr("plan-id"), zone);
            }
        });
        $(zone + " .box-list-planOther .plan-del").off();
        $(zone + " .box-list-planOther .plan-del").on("click", function () {
            if (isProfile) {
                pinJs.delPlan($(this).attr("plan-id"), zone, true);
            } else {
                pinJs.delPlan($(this).attr("plan-id"), zone);
            }
        });
    },
    resetFormPlan: function (infoForm) {
        $(infoForm).trigger("reset");
        $(infoForm + " #PlanID").val(0);
        $(infoForm + " .field-validation-error").html("");
        $(infoForm + " .field-validation-valid").html("");
        $(infoForm + " .box-plan-form").find("input").removeClass("input-validation-error");
        $(infoForm + " .box-plan-form").find("select").removeClass("input-validation-error");
        //img
        $(infoForm + " #img-preview-thumbnail").hide();
        $(infoForm + " #ThumbnailUrl").val("");
        $(infoForm + " #flag-img").val("");

        pinJs.setRadioCheck(infoForm + " #radRecurringBilling", true);
        pinJs.setRadioCheck(infoForm + " #radOnTimePayment", false);
        $(infoForm + " #IsRecurringBilling").val(true);
    },
    editPlan: function (id, isProfile, formId, zone) {
        if (isProfile) {
            $(formId + " .k-delete").hide();
            //console.log(formId);
            $(formId + " .box-plan-form").find("input").removeClass("input-validation-error");
            $(formId + " .box-plan-form").find("select").removeClass("input-validation-error");
            $(formId + " .box-plan-form").find("span.field-validation-error").html("");

            $(formId + " .title-header").html("Edit Plan");
            $(formId + " #PlanID").val($(zone + " #plan-planId-" + id).val());
            $(formId + " #Name").val($(zone + " #plan-name-" + id).attr("value"));
            $(formId + " #Price").val($(zone + " #plan-price-" + id).val());
            var typeId = $(zone + " #plan-typeId-" + id).val();
            $(formId + " #TypeID").val(typeId);
            $(formId + " #PromoCode").val($(zone + " #plan-PromoCode-" + id).val());
            $(formId + " #PromotionalDiscountPercentage").val($(zone + " #plan-PromotionalDiscountPercentage-" + id).val());
            $(formId + " #PromotionalDiscountCash").val($(zone + " #plan-PromotionalDiscountCash-" + id).val());
            $(formId + " #PromotionalEndDate").val($(zone + " #plan-PromotionalEndDate-" + id).val());

            var fileName = $(zone + " #plan-thumbnail-" + id).val();
            $(formId + " #ThumbnailUrl").val(fileName);
            $(formId + " #NumberOfLesson").val($(zone + " #plan-numberOfLesson-" + id).val());
            $(formId + " #LessonPackage").val($(zone + " #plan-lessonPackage-" + id).val());
            $(formId + " #PromoPercentage").val($(zone + " #plan-promo-" + id).val());
            $(formId + " #LengthOfTime").val($(zone + " #plan-lengthOfTime-" + id).val());
            //SuNV added 2016-10-13
            $(formId + " #ListPaymentMethodID").val($(zone + " #plan-ListPaymentMethodID-" + id).val());
            $(formId + " #RecurringMethodID").val($(zone + " #plan-RecurringMethodID-" + id).val());
            $(formId + " #ProratedFee").val($(zone + " #plan-ProratedFee-" + id).val());
            $(formId + " #payment-method-name").html($(zone + " #plan-listPaymentNameDsp-" + id).val());
            $(formId + " #ListShowOnWidget").val($(zone + " #plan-ListShowOnWidget-" + id).val());

            $(formId + " #NumberPerPackage").val($(zone + " #plan-NumberPerPackage-" + id).val());
            var IsNotRequiredTeacher = $(zone + " #plan-IsNotRequiredTeacher-" + id).val();
            var vIsNotRequiredTeacher = (IsNotRequiredTeacher == 1) ? true : false;
            var IsNotRequiredInstrument = $(zone + " #plan-IsNotRequiredInstrument-" + id).val();
            var vIsNotRequiredInstrument = (IsNotRequiredInstrument == 1) ? true : false;
            $(formId + " #IsNotRequiredTeacher").val(vIsNotRequiredTeacher);
            $(formId + " #IsNotRequiredInstrument").val(vIsNotRequiredInstrument);

            $(zone + ' #IsNotRequiredTeacher').prop('checked', vIsNotRequiredTeacher);
            $(zone + ' #IsNotRequiredInstrument').prop('checked', vIsNotRequiredInstrument);

            if ($(zone + " #plan-RecurringMethodID-" + id).val() == 2) {
                $(zone + " .row-number-of-lesson").show();
            } else {
                $(zone + " .row-number-of-lesson").hide();
            }
            //End

            $(formId + " #GiftCard").val($(zone + " #plan-giftCard-" + id).val());
            var isRecuring = $(zone + " #plan-IsRecurringBilling-" + id).val();

            //SuNV update 2016-09-20 TZ-2835
            var isNoEndDate = $(zone + " #plan-IsNoEndDate-" + id).val();
            //End

            $(formId + " #IsRecurringBilling").val(isRecuring);
            if (isRecuring == "1") {
                $("#frmCardBuilder #IsRecurringBilling").val(true);
                pinJs.setRadioCheck(formId + " #radRecurringBilling", true);
                pinJs.setRadioCheck(formId + " #radOnTimePayment", false);
                //sunv addded 2016-10-13
                $(formId + " .row-recuring-method").show();
            }
            else if (isRecuring == '0') {
                $("#frmCardBuilder #IsRecurringBilling").val(false);
                pinJs.setRadioCheck(formId + " #radRecurringBilling", false);
                pinJs.setRadioCheck(formId + " #radOnTimePayment", true);
                //sunv addded 2016-10-13
                $(formId + " .row-recuring-method").hide();
            } else {
                $("#frmCardBuilder #IsRecurringBilling").val(true);
                pinJs.setRadioCheck(formId + " #radRecurringBilling", false);
                pinJs.setRadioCheck(formId + " #radOnTimePayment", false);
                //sunv addded 2016-10-13
                $(formId + " .row-recuring-method").show();
            }

            var IsNotRequired = $(zone + " #plan-IsNotRequired-" + id).val();
            if (IsNotRequired == "1") {
                $(zone + ' #IsNotRequired').prop('checked', true);
            } else {
                $(zone + ' #IsNotRequired').prop('checked', false);
            }

            //SuNV update 2016-09-20 no-end-date
            if (isNoEndDate == "1") {
                $("#frmCardBuilder #IsNoEndDate").val(true);
                pinJs.setRadioCheck(formId + " #radNoEndDate", true);
            }
            else if (isRecuring == '0') {
                $("#frmCardBuilder #IsRecurringBilling").val(false);
                pinJs.setRadioCheck(formId + " #radNoEndDate", false);
            }
            //end

            $(formId + " .btn-save-card-builder").hide();
            $(formId + " .btn-update-card-builder").show();
            $(formId + " .tile-header").html("Edit Plan");
            //
            var img = $(zone + " #frmCardBuilder #ThumbnailUrl").val();
            if (fileName != "") {
                $(formId + " #panelThumb").find(".k-dropzone").removeClass("dsp-none");
                $(formId + " #img-preview-thumbnail").show();
                $(formId + " #img-preview-thumbnail").attr("src", $(zone + ' #hidPathRootS3').val() + fileName);
                if ($(formId + ' #flag-img').val() != 1) {
                    $(formId + " #img-preview-thumbnail").removeClass("dsp-none");
                }
                $(formId + " #dspFileImg").show();
            } else {
                $(formId + " #dspFileImg").hide();
                $(formId + " #img-preview-thumbnail").hide();
                $(formId + " #img-preview-thumbnail").attr("src", "");
            }

            if (typeId == "1") {
                $(zone + " .row-number-lesson").show();
                $(zone + " .row-lesson-package").hide();
                $(zone + " #LessonPackage").val("1");
            } else {
                $(zone + " .row-number-lesson").hide();
                $(zone + " .row-lesson-package").show();
            }

            //sunv fixed for no end date
            if ($(zone + ' #IsNoEndDate').val() === 'true') {
                $(zone + ' #radNoEndDate').prop('checked', true);
                $(zone + ' #txtLessonPackage').removeClass('hidden');
                $(zone + ' #LessonPackage').addClass('hidden');
            } else {
                $(zone + ' #radEndAfter').prop('checked', true);
                $(zone + ' #LessonPackage').removeClass('hidden');
                //$('#popupPlanInfo #LessonPackage').val($('#popupPlanInfo #txtLessonPackage').val());
                $(zone + ' #txtLessonPackage').addClass('hidden');
            }

            if ($(zone + ' #radNoEndDate').is(':checked')) {
                $(zone + ' #txtLessonPackage').removeClass('hidden');
                $(zone + ' #LessonPackage').addClass('hidden');
                //sunv addded 2016-09-20
                $("#popupPlanInfo #IsNoEndDate").val(true);
                //end
            } else {
                $(zone + ' #LessonPackage').removeClass('hidden');
                //$('#popupPlanInfo #LessonPackage').val($('#txtLessonPackage').val());
                $(zone + ' #txtLessonPackage').addClass('hidden');
                //sunv addded 2016-09-20
                $("#popupPlanInfo #IsNoEndDate").val(false);
                //end
            }

            // if is single lesson->only show One-Time Payment
            if (typeId == 1) {
                $(zone + " .row-recurring-billing").hide();
                $(zone + " #IsRecurringBilling").val(false);
                $(zone + " #radRecurringBilling").prop('checked', false);
                $(zone + " #radOnTimePayment").prop('checked', true);
            } else {
                $(zone + " .row-recurring-billing").show();
            }

            //set attend
            $.ajax({
                url: "/CardBuilder/GetListAttendanceTypeByPlan/" + id,
                type: "GET",
                dataType: "json",
                //async: false,
                //data: {
                //    id: planId
                //},
                success: function (response) {
                    $(zone + " #Attended").prop('checked', response.Attended);
                    $(zone + " #StudentCancelled").prop('checked', response.StudentCancelled);
                    $(zone + " #TeacherCancelled").prop('checked', response.TeacherCancelled);
                    $(zone + " #NoShow").prop('checked', response.NoShow);
                    $(zone + " #Banked").prop('checked', response.Banked);
                },
                error: function (xhr, ajaxOptions, thrownError) {
                }
            });
            //end

        } else {
            $("#popPlanInfo").remove();
            var options = {
                id: "popPlanInfo",
                width: 700,
                title: false,
                draggable: false,
                resizable: false
            };
            var popupWindow = pinJs.initPopupWindow(options);
            popupWindow.refresh({
                url: "/CardBuilder/Edit/" + id,
                width: 700,
                async: false
            }).center().open();
            $('#popPlanInfo').addClass('scroll');
            $("#popPlanInfo").parent().addClass("mobileModal");
            //init form
            //pinJs.initFormWindow("frmEditCardBuilder", popupWindow);   
        }
    },
    setRadioCheck: function (_element, bValue) {
        $(_element).prop('checked', bValue);
    },
    delPlan: function (planId, zone, isProfile) {
        window.pinJs.msgConfirm({
            text: "Are you sure that you want to delete this Plan?",
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        $.ajax({
                            url: "/CardBuilder/Delete/" + planId,
                            type: "POST",
                            dataType: "json",
                            //async: false,
                            data: {
                                id: planId
                            },
                            success: function (response) {
                                if (response.Success == 1) {
                                    if (response.Total == "0") {
                                        $(zone + " .group-plans").hide();
                                    } else {
                                        $(zone + " .group-plans").show();
                                    }
                                    //ContentHTML
                                    if (isProfile) {
                                        $(zone + " .box-list-plan").html(response.ContentHTML);
                                        pinJs.actionPlan(zone, true, zone + " #frmCardBuilder");
                                    } else {
                                        $(zone + " .region-plans").html(response.ContentHTML);
                                        pinJs.actionPlan(zone);
                                    }
                                    $(zone + " .box-update-plan").hide();
                                } else {
                                    window.pinJs.msgError(response.ErrorMsg);
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },
    onChangeTypPlan: function (zone, idSingleLesson) {
        if ($(zone + " #TypeID").val() == idSingleLesson || $(zone + " #TypeID").val() == '') {
            $(zone + " .row-number-lesson").show();
            if ($(zone + " #TypeID").val() == '') {
                $(zone + " .row-number-lesson").hide();
            }
            $(zone + " .row-lesson-package").hide();
            if ($(zone + " #TypeID").val() == idSingleLesson) {
                $(zone + " #LessonPackage").val(1);//to pass valid   
            }
            //SuNV updated: TZ-3516 [2016-12-28] 
            if ($(zone + " #TypeID").val() == idSingleLesson) {
                $(zone + " .row-recurring-billing").hide();
                $(zone + " #IsRecurringBilling").val(false);
                $(zone + " #radRecurringBilling").prop('checked', false);
                $(zone + " #radOnTimePayment").prop('checked', true);
                $(zone + " #RecurringMethodID").val(1);
            } else {
                $(zone + " .row-recurring-billing").show();
                $(zone + " .group-recurring-billing").show();
                pinJs.setRadioCheck(zone + " #radRecurringBilling", true);
                pinJs.setRadioCheck(zone + " #radOnTimePayment", false);
            }
            //End
        } else {
            //SuNV updated: TZ-3516 [2016-12-28] 
            $(zone + " .row-recurring-billing").show();
            pinJs.setRadioCheck(zone + " #radRecurringBilling", true);
            pinJs.setRadioCheck(zone + " #radOnTimePayment", false);

            $(zone + " .row-number-lesson").hide();
            $(zone + " #NumberOfLesson").val("");
            $(zone + " .row-lesson-package").show();
            //$(zone + " #LessonPackage").val(100);
            if ($(zone + ' #radNoEndDate').is(':checked')) {
                $(zone + ' #txtLessonPackage').removeClass('hidden');
                $(zone + ' #LessonPackage').addClass('hidden');
                $(zone + ' #LessonPackage').val(100);
            } else {
                $(zone + ' #LessonPackage').removeClass('hidden');
                $('#LessonPackage').val($('#txtLessonPackage').val());
                $(zone + ' #txtLessonPackage').addClass('hidden');
            }
        }
        //recurring
        if ($(zone + " #radRecurringBilling").is(":checked")) {
            if ($(zone + " #TypeID").val() != idSingleLesson) {
                $(zone + " .group-recurring-billing").show();
                $(zone + " #IsRecurringBilling").val(true);
            } else {
                $(zone + " .group-recurring-billing").hide();
                $(zone + " #IsRecurringBilling").val(false);
            }
        } else {
            $(zone + " .group-recurring-billing").hide();
        }
    },
    validRecurring: function (zone) {
        var value = $(zone + " #IsRecurringBilling").val();
        if (value == "") {
            $(zone + " #msg-valid-recurring").removeClass("hidden");
            $(zone + " #msg-valid-recurring").html("The Payment Type is required");
        } else {
            $(zone + " #msg-valid-recurring").addClass("hidden");
        }
    },
    onlyInputNumber: function (fileName) {
        $(fileName).keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
    },
    validRequiredPlan: function (fieldName, obj) {
        if (obj.val() == "") {
            alertify.error('The ' + fieldName + ' is required!');
            obj.addClass('input-validation-error');
            return true;
        }
        return false;
    },
    validRequired: function (fieldName, obj) {
        if (obj.val() == "") {
            alertify.error('The ' + fieldName + ' is required');
            obj.addClass('input-validation-error');
            obj.focus();
            return false;
        } else {
            obj.removeClass('input-validation-error');
        }
        return true;
    },
    setValueRecuring: function (zone) {
        $(zone + " input:radio[name=RecurringBilling]").on("change", function () {
            var valueChecked = $(zone + " input:radio[name=RecurringBilling]:checked").val();
            $(zone + " #IsRecurringBilling").val(valueChecked == "1" ? true : false);
            //sunv addded 2016-10-11
            if (valueChecked == "1") {
                $(zone + " .row-recuring-method").show();
            } else {
                $(zone + " .row-recuring-method").hide();
            }
            //End
        });
    },
    setValueNoEndDate: function (zone) {
        $(zone + " input:radio[name=radNoEndDate]").on("change", function () {
            var valueChecked = $(zone + " input:radio[name=radNoEndDate]:checked").val();
            $(zone + " #IsNoEndDate").val(valueChecked == "1" ? true : false);
        });
    },
    planOption: function (arrShow, arrHide, idSchedule, cssHide) {
        $.each(arrShow, function (index, value) {
            $("#frmAddOrEditMember " + value + "-" + idSchedule).removeClass(cssHide);
        });
        $.each(arrHide, function (index, value) {
            $("#frmAddOrEditMember " + value + "-" + idSchedule).addClass(cssHide);
        });
    },
    initFormWindowMessage: function (formId, popupWindow, grid, callback, typeId) {
        var $form = $(formId);

        $form.on('keyup keypress', function (e) {
            var code = e.keyCode || e.which;
            if (code == 13) {
                e.preventDefault();
                return false;
            }
        });

        //handler button
        $form.find(".k-grid-cancel").on("click", function () {
            //var messId = $form.find('input[name="MessageID"]').val();            
            if (popupWindow) popupWindow.close();
            // window.pinJs.subInboxCountMess(messId);
        });

        $form.find('.k-grid-reply').on('click', function () {
            var frmname = $form.find('input[name="FromUserName"]').val();
            var frmsub = $form.find('#TitleText').text() || '';
            var messId = $form.find('input[name="MessageID"]').val();
            if (popupWindow) popupWindow.close();
            window.pinJs.subInboxCountMess(messId);
            var options = {
                id: 'messageAddOrEdit',
                width: 900,
                title: false
            };
            popupWindow = window.pinJs.initPopupWindow(options);
            popupWindow.refresh({
                url: '/Message/Text' + '?typeId=' + $('#hidTypeId').val() + '&tun=' + frmname + '&resub=' + frmsub,
                async: false
            }).center().open();
            window.pinJs.initFormWindowMessage('#frmAddOrEditMessage', popupWindow, grid);
        });

        //fix close for modal
        $(".content-modal").find(".k-grid-cancel").on("click", function () {
            var messId = $form.find('input[name="MessageID"]').val();
            if (popupWindow) popupWindow.close();
            window.pinJs.subInboxCountMess(messId);
        });

        $form.find(".k-grid-update").on("click", function () {
            var messId = $form.find('input[name="MessageID"]').val();
            $form.submit();
            window.pinJs.subInboxCountMess(messId);
        });
        //handler post form
        $.validator.unobtrusive.parse($form);
        var validator = $form.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $('#btn-save').attr('disabled', 'disabled');
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                success: function (result, textStatus, jqXHR) {
                    if (result.Success == true) {
                        if (popupWindow) popupWindow.close();
                        if (callback && typeof (callback) == 'function') {
                            callback(result);
                        } else {
                            if (grid) {
                                var gridData = grid.data("kendoGrid");
                                if (gridData) gridData.dataSource.read();
                            }
                        }
                        if (typeId) {
                            $.connection.webRtcHub.server.sendMessageSuccess(result.ToMemId, typeId);
                        } else {
                            $.connection.webRtcHub.server.sendMessageSuccess(result.ToMemId, $('#hidTypeId').val());
                        }
                    } else {
                        validator.showErrors(result.Errors);
                        $('#btn-save').removeAttr('disabled');
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //if fails
                    $('#btn-save').removeAttr('disabled');
                }
            });
        };
        //focus first text box
        window.pinJs.focusFirstInputForm($form);
    },
    dataExportTempMember: function (objData) {
        $.ajax(
            {
                url: '/Data/ExportTemplateMember/',
                type: "GET",
                data: {
                    obj: objData
                },
                beforeSend: function (xhr, opts) {
                    //$(".import-member-processing").show();
                },
                success: function (data) {
                    if (data.Success == 1) {
                        //$("#link-result-csv-import-member").attr('href', data.FileName);
                        //$(".import-member-download").show();
                        window.location.href = data.FileName;

                    } else {
                        alertify.error(data.Message);
                    }

                    $(".import-member-processing").hide();
                    return false;
                }
            });
    },
    arrayToString: function (lst) {
        var lstTeacherId = "";
        var listTeacher = lst;
        for (var i = 0; i < listTeacher.length; i++) {
            //lstTeacherId.push(listTeacher[i].Value);
            if (i == 0) {
                lstTeacherId = listTeacher[i].Value;
            } else {
                lstTeacherId = lstTeacherId + "," + listTeacher[i].Value;
            }
        }
        return lstTeacherId;
    },
    importMemberFromCSV: function (_filename, _required, _adminid, _lstSchoolId, _lstTeacherId, zone, membertypeid) {
        var lstTeacherId = pinJs.arrayToString(_lstTeacherId);
        var lstSchoolId = pinJs.arrayToString(_lstSchoolId);
        $.ajax(
            {
                url: '/Data/ImportMemberToDB/',
                type: "GET",
                data: {
                    filename: _filename,
                    required: _required,
                    adminId: _adminid,
                    arrSchoolId: lstSchoolId,
                    arrtTecherId: lstTeacherId,
                    membertype: membertypeid
                },
                beforeSend: function (xhr, opts) {
                    $(zone + " .import-member-processing").show();
                    $(zone + " .msg-import-member-successfull").hide();
                    $(zone + " .msg-import-member-fail").hide();
                    $(zone + " #data-upload-template-member").attr("disabled", "disabled");
                },
                success: function (data) {
                    $(zone + " #data-upload-template-member").removeAttr("disabled", "disabled");
                    $(zone + " .import-member-processing").hide();
                    if (data.Success == 1) {
                        $(zone + " .msg-import-member-empty").hide();
                        $(zone + " #count-import-successfull").html(data.ImportSuccess);
                        $(zone + " .msg-import-member-successfull").show();
                        if (data.ImportSuccess > 1) {
                            $(".fix-spell-ok").html("s");
                        }
                        if (data.ImportFail != '0') {
                            $("#count-import-fail").html(data.ImportFail);
                            $(".link-import-member-fail").attr('href', data.FileNameError);
                            $(".msg-import-member-fail").show();
                            if (data.ImportFail > 1) {
                                $(".fix-spell-fail").html("s");
                            }
                        }
                    } else {
                        if (data.Success == 2) {
                            //$(".msg-import-member-empty").html(data.Message);
                            $(zone + " .msg-import-member-empty").show();
                        } else {
                            alertify.error(data.Message);
                        }
                    }
                }
            });
    },
    importMemberOnChangeSchool: function () {
        //Get teacher by school
        var lst = [];
        var listSchool = $(".data-import-member #ListSchoolID").data("kendoMultiSelect").dataItems();
        for (var i = 0; i < listSchool.length; i++) {
            lst.push(listSchool[i].Value);
        }
        $.ajax({
            url: '/Member/GetTecherBySchool',
            type: "POST",
            dataType: "json",
            data: {
                listSchool: lst
            },
            success: function (response) {
                if (response.StatusID == 1 || response.StatusID == 0) {
                    var dataSource = new kendo.data.DataSource({
                        data: JSON.parse(response.Data)
                    });
                    var multiselect = $('.data-import-member #ListTeacherID').data("kendoMultiSelect");
                    multiselect.setDataSource(dataSource);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
        //}        
    },
    getPaymentMethodByPlan: function () {
        //Get teacher by school
        var planId = $("#frmAddOrEditBookingPlan #PlanIDBooking").val();
        var teacherId = $("#frmAddOrEditBookingPlan #BookingTeacherID").val();
        var studentId = $("#frmAddOrEditBookingPlan #StudentID").val();

        $.ajax({
            url: '/Member/GetPaymentMethodOfPlan',
            type: "GET",
            dataType: "json",
            data: {
                id: planId
            },
            success: function (response) {
                if (response.Success == true) {
                    var dataSource = new kendo.data.DataSource({
                        data: JSON.parse(response.Data)
                    });
                    var objSelect = $("#frmAddOrEditBookingPlan #MemberPaymentIDBooking").data("kendoComboBox");
                    objSelect.setDataSource(dataSource);
                    objSelect.select(0);
                    $("#frmAddOrEditBookingPlan .booking-choose-card").show();

                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });

        $.ajax({
            url: '/Member/GetListScheduleRefByTeacherId',
            type: "GET",
            dataType: "json",
            data: {
                studentId: studentId,
                teacherId: teacherId,
                planId: planId,
                timeOffset: window.pinJs.getTimezoneOffsetNoDST()
            },
            success: function (response) {
                if (response.Success == true) {
                    var dataSource = new kendo.data.DataSource({
                        data: JSON.parse(response.Data)
                    });
                    //var objSelect = $("#frmAddOrEditBookingPlan #BookingScheduleReferenceID").data("kendoComboBox");
                    //objSelect.setDataSource(dataSource);
                    //$("#frmAddOrEditBookingPlan .container-booking-choose-schedule-ref").removeClass('hidden');
                    //objSelect.select(0);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    },
    getPlanByTeacherId: function () {
        //Get teacher by school
        var teacherId = $("#frmAddOrEditBookingPlan #BookingTeacherID").val();
        var studentId = $("#frmAddOrEditBookingPlan #StudentID").val();
        var planId = $("#frmAddOrEditBookingPlan #PlanIDBooking").val();

        $.ajax({
            url: '/Member/GetListPlanOfTeacher',
            type: "GET",
            dataType: "json",
            data: {
                id: teacherId
            },
            success: function (response) {
                if (response.Success == true) {
                    var dataSource = new kendo.data.DataSource({
                        data: JSON.parse(response.Data)
                    });
                    var objSelect = $("#frmAddOrEditBookingPlan #PlanIDBooking").data("kendoComboBox");
                    objSelect.setDataSource(dataSource);
                    //objSelect.select(0);                    

                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });

        $.ajax({
            url: '/Member/GetListScheduleRefByTeacherId',
            type: "GET",
            dataType: "json",
            data: {
                studentId: studentId,
                teacherId: teacherId,
                planId: planId,
                timeOffset: window.pinJs.getTimezoneOffsetNoDST()
            },
            success: function (response) {
                if (response.Success == true) {
                    var dataSource = new kendo.data.DataSource({
                        data: JSON.parse(response.Data)
                    });
                    //var objSelect = $("#frmAddOrEditBookingPlan #BookingScheduleReferenceID").data("kendoComboBox");
                    //objSelect.setDataSource(dataSource);
                    //$("#frmAddOrEditBookingPlan .container-booking-choose-schedule-ref").removeClass('hidden');
                    //objSelect.select(0);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
    },
    getScheduleRefByTeacherId: function () {
        //Get teacher by school
        var teacherId = $("#frmMemberBillingInfo #TeacherID").val();
        var studentId = $("#frmMemberBillingInfo #MemberID").val();
        var billingId = $("#frmMemberBillingInfo #MemberBillingInfoID").val();
        $.ajax({
            url: '/Member/GetListScheduleRefByTeacherId',
            type: "GET",
            dataType: "json",
            data: {
                studentId: studentId,
                teacherId: teacherId,
                billingId: billingId,
                timeOffset: window.pinJs.getTimezoneOffsetNoDST()
            },
            success: function (response) {
                if (response.Success == true) {
                    var dataSource = new kendo.data.DataSource({
                        data: JSON.parse(response.Data)
                    });
                    var objSelect = $("#frmMemberBillingInfo #ScheduleReferenceID").data("kendoComboBox");
                    objSelect.setDataSource(dataSource);
                    $("#frmMemberBillingInfo #ContainerScheduleReferenceID").removeClass('hidden');
                    objSelect.select(0);
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
        //}        
    },
    getValueCheckbox: function (elementHtml) {
        return $(elementHtml).is(':checked') ? 1 : 0;
    },
    //Case Modal
    initFormWindowModal: function (formId, modalWindown, grid, callback) {
        var $form = $(formId);
        //handler button
        $form.find(".k-grid-cancel").on("click", function () {
            if (modalWindown) $(modalWindown).modal('hide');
            clearForm(formId);
            $(modalWindown).find(".k-upload").addClass("k-upload-empty");
            $(modalWindown).find('.k-upload-files').remove();
            $(modalWindown).find('.k-upload-status').remove();
        });

        $form.find(".k-grid-update").on("click", function () {
            $form.submit();
        });
        //handler post form
        $.validator.unobtrusive.parse($form);
        var validator = $form.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                success: function (result, textStatus, jqXHR) {
                    if (result.Success == true) {
                        if (modalWindown) $(modalWindown).modal('hide');
                        clearForm(formId);
                        $(modalWindown).find(".k-upload").addClass("k-upload-empty");
                        $(modalWindown).find('.k-upload-files').remove();
                        $(modalWindown).find('.k-upload-status').remove();
                        //if (popupWindow) popupWindow.close();
                        if (callback && typeof (callback) == 'function') {
                            callback(result);
                        } else {
                            if (grid) {
                                var gridData = grid.data("kendoGrid");
                                if (gridData) gridData.dataSource.read();
                            }
                        }
                    } else {
                        validator.showErrors(result.Errors);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    //if fails
                }
            });
        };
        //focus first text box
        pinJs.focusFirstInputForm($form);
    },
    focusFirstInputForm: function ($form) {
        setTimeout(function () {
            var txtInput = $form.find('input[type=text]:first');
            if (txtInput.length == 0) return;
            var strLength = txtInput.val().length + 1;
            txtInput.focus();
            txtInput[0].setSelectionRange(strLength, strLength);
        }, 500);
    },

    setaGridDel: function (record, ui) {
        var oGrid = $(ui.grid).data("kendoGrid");
        var row = $(record).closest("tr");
        var dataItem = oGrid.dataItem(row);
        var id = dataItem[ui.recId];
        window.pinJs.msgConfirm({
            text: ui.confirmDel,
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        $.ajax({
                            url: ui.urlDel,
                            type: "POST",
                            dataType: "json",
                            //async: false,
                            data: {
                                id: id
                            },
                            success: function (response) {
                                if (response.Status == 1) {
                                    oGrid.dataSource.read();
                                } else {
                                    window.pinJs.msgError(response.ErrorMsg);
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },
    setaDelItem: function (id, ui, callback) {
        window.pinJs.msgConfirm({
            text: ui.confirmDel,
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        $.ajax({
                            url: ui.urlDel,
                            type: "POST",
                            dataType: "json",
                            //async: false,
                            data: {
                                id: id
                            },
                            success: function (response) {
                                if (response.Status == 1) {
                                    if (callback) {
                                        callback(response);
                                    } else {
                                        //                                        location.href = ui.urlCallback;
                                        var filter = $("#btn-group-filter-by-name");
                                        if (filter != undefined) {
                                            filter.trigger("click");
                                        }
                                        var container = $("#list-group-student");
                                        if (container != undefined) {
                                            var page = container.attr("page");
                                            var pageSize = container.attr("page-size");
                                            if (page == undefined) page = 1;
                                            if (pageSize == undefined) pageSize = 4;
                                            var keyword = $("#group-filter-by-name").val();
                                            if (keyword == undefined) keyword = "";
                                            pinJs.searchGroupStudents('list-group-student', 'group-load-more', page, pageSize, keyword);
                                        }
                                    }
                                } else {
                                    window.pinJs.msgError(response.ErrorMsg);
                                }

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },
    setaDelItemPending: function (id, ui, callback) {
        window.pinJs.msgConfirm({
            text: ui.confirmDel,
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        $.ajax({
                            url: ui.urlDelPending,
                            type: "POST",
                            dataType: "json",
                            //async: false,
                            data: {
                                id: id
                            },
                            success: function (response) {
                                if (response.Status == 1) {
                                    if (callback) {
                                        callback(response);
                                    } else {
                                        location.href = ui.urlCallback;
                                    }
                                } else {
                                    window.pinJs.msgError(response.ErrorMsg);
                                }
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },
    delPaymentMethod: function (id, htmlShow, zone, isEdit, memberId) {
        window.pinJs.msgConfirm({
            text: "Are you sure that you want to delete this payment?",
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        $.ajax({
                            url: "/Payment/Delete",
                            type: "POST",
                            dataType: "json",
                            //async: false,
                            data: {
                                id: id,
                                studentId: memberId
                            },
                            success: function (response) {
                                if (response.Success == 1) {
                                    $(zone + " #payment-box-update").hide();
                                    $(zone + " #billing-box-update").hide();
                                    $(zone + " #billing-box-edit").hide();
                                    $(zone + " #payment-box-edit").hide();
                                    $(zone + " #ach-payment-method").hide();

                                    if (response.Total == "0") {
                                        $(zone + " .group-payment").hide();
                                    } else {
                                        $(zone + " .group-payment").show();
                                    }
                                    $(zone + " #payment-box-update").hide();
                                    $(zone + " #billing-box-update").hide();
                                    $(htmlShow).html(response.ContentPaymentHTML);
                                    if (isEdit) {
                                        pinJs.actionPayment2(zone);
                                    } else {
                                        pinJs.actionPayment(zone);
                                    }
                                    $(zone + " .group-close").show();
                                } else {
                                    window.pinJs.msgError(response.Message);
                                }

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },
    delBillingAddress: function (id, htmlShow, zone, isEdit) {
        window.pinJs.msgConfirm({
            text: "Are you sure that you want to delete this billing address?",
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        $.ajax({
                            url: "/Payment/DeleteBillingAddress",
                            type: "POST",
                            dataType: "json",
                            //async: false,
                            data: {
                                id: id
                            },
                            success: function (response) {
                                if (response.Success == 1) {
                                    $(zone + " #payment-box-update").hide();
                                    $(zone + " #payment-box-edit").hide();
                                    $(zone + " #billing-box-update").hide();
                                    if (response.Total == "0") {
                                        $(zone + " .group-billing").hide();
                                    } else {
                                        $(zone + " .group-billing").show();
                                    }
                                    $(zone + " #payment-box-update").hide();
                                    $(zone + " #billing-box-update").hide();
                                    $(htmlShow).html(response.ContentPaymentHTML);
                                    if (isEdit) {
                                        pinJs.actionBillingAddress2(zone);
                                    } else {
                                        pinJs.actionBillingAddress(zone);
                                    }
                                    //$(zone + " #payment-box-update").hide();
                                    //$(zone + " #billing-box-update").hide();
                                    $(zone + " #billing-box-edit").hide();
                                    $(zone + " #payment-box-edit").hide();
                                    $(zone + " .group-close").show();
                                } else {
                                    window.pinJs.msgError(response.Message);
                                }

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },
    delRoom: function (id, htmlShow, zone) {
        window.pinJs.msgConfirm({
            text: "Are you sure that you want to delete this room?",
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        $.ajax({
                            url: "/ProfileInformation/DeleteRoom",
                            type: "POST",
                            dataType: "json",
                            //async: false,
                            data: {
                                id: id
                            },
                            success: function (response) {
                                if (response.Success == 1) {
                                    if (id == $(zone + " #RoomID").val()) {
                                        $(zone + " .box-update-room").hide();
                                        pinJs.resetFormRoom("#frmRoomInfo");
                                    }

                                    if (response.Total == "0") {
                                        $(zone + " .group-list-room").hide();
                                    } else {
                                        $(htmlShow).html(response.ContentHTML);
                                        $(zone + " .group-list-room").show();
                                    }
                                } else {
                                    window.pinJs.msgError(response.Message);
                                }

                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },
    planModalReason: function (text, titleHeader) {
        window.pinJs.msgAlerModal({
            text: text,
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        }, titleHeader);
    },
    editPaymentMethod: function (id, formId) {
        $(formId + " #CardNumber").val($(".card-number-" + id).attr("value"));

        var expDate = $(".card-exp-" + id).attr("value");
        var arrDate = expDate.split("/");
        $(formId + " #MonthExp").val(arrDate[0]);
        $(formId + " #YearExp").val(arrDate[1]);
        //$(formId + " #ExpirationDate").val($(".card-exp-" + id).attr("value"));

        $(formId + " #FullName").val($(".card-name-" + id).html());
        $(formId + " #Cvv").val($("#card-code-" + id).val());
        $(formId + " #PaymentMethodID").val(id);
        $(formId + " #Token").val($("#card-token-" + id).val());

        $(formId + " #BillingFullName").val($("#card-bFullname-" + id).val());
        $(formId + " #BillingEmail").val($("#card-bEmail-" + id).val());
        $(formId + " #BillingCountry").val($("#card-bCountry-" + id).val());
        $(formId + " #BillingCity").val($("#card-bCity-" + id).val());
        $(formId + " #BillingState").val($("#card-bState-" + id).val());
        $(formId + " #BillingZipCode").val($("#card-bZipCode-" + id).val());
        $(formId + " #BillingAddress1").val($("#card-bAddress1-" + id).val());
        $(formId + " #BillingAddress2").val($("#card-bAddress2-" + id).val());
        $(formId + " #BillingPhone").val($("#card-bPhone-" + id).val());

        $(formId + " .btn-save-payment").hide();
        $(formId + " .btn-update-payment").show();
        $(formId + " .tile-header").html("Edit Credit Card");
    },
    editAchPaymentMethod: function (id, formId) {

        $(formId + " #CheckName").val($("#card-check-name-" + id).val());
        $(formId + " #CheckAba").val($("#card-check-aba-" + id).val());
        $(formId + " #CheckAccount").val($("#card-check-account-" + id).val());
        $(formId + " #AccountHolderTypeID").val($("#card-check-accountholder-" + id).val());
        $(formId + " #AccountTypeID").val($("#card-check-accounttype-" + id).val());
        $(formId + " #SecCode").val($("#card-check-seccode-" + id).val());

        $(formId + " #PaymentMethodID").val(id);
        $(formId + " #Token").val($("#card-token-" + id).val());

        $(formId + " #BillingFullName").val($("#card-bFullname-" + id).val());
        $(formId + " #BillingEmail").val($("#card-bEmail-" + id).val());
        $(formId + " #BillingCountry").val($("#card-bCountry-" + id).val());
        $(formId + " #BillingCity").val($("#card-bCity-" + id).val());
        $(formId + " #BillingState").val($("#card-bState-" + id).val());
        $(formId + " #BillingZipCode").val($("#card-bZipCode-" + id).val());
        $(formId + " #BillingAddress1").val($("#card-bAddress1-" + id).val());
        $(formId + " #BillingAddress2").val($("#card-bAddress2-" + id).val());
        $(formId + " #BillingPhone").val($("#card-bPhone-" + id).val());

        $(formId + " .btn-save-ach-payment").hide();
        $(formId + " .btn-update-ach-payment").show();
        //$(formId + " .tile-header").html("Edit Credit Card");
    },
    editPaymentMethod2: function (id, formId) {
        $(formId + " #CardNumber").val($(".card-number-" + id).attr("value"));

        var expDate = $(".card-exp-" + id).attr("value");
        var arrDate = expDate.split("/");
        $(formId + " #MonthExp").val(arrDate[0]);
        $(formId + " #YearExp").val(arrDate[1]);
        $(formId + " #FullName").val($(".card-name-" + id).html());
        $(formId + " #Cvv").val($("#card-code-" + id).val());
        $(formId + " #PaymentMethodID").val(id);
        $(formId + " #Token").val($("#card-token-" + id).val());

        $(formId + " #BillingFullName").val($("#card-bFullname-" + id).val());
        $(formId + " #BillingEmail").val($("#card-bEmail-" + id).val());
        $(formId + " #BillingCountry").val($("#card-bCountry-" + id).val());
        $(formId + " #BillingCity").val($("#card-bCity-" + id).val());
        $(formId + " #BillingState").val($("#card-bState-" + id).val());
        $(formId + " #BillingZipCode").val($("#card-bZipCode-" + id).val());
        $(formId + " #BillingAddress1").val($("#card-bAddress1-" + id).val());
        $(formId + " #BillingAddress2").val($("#card-bAddress2-" + id).val());
        $(formId + " #BillingPhone").val($("#card-bPhone-" + id).val());

        $(formId + " .btn-save-payment").hide();
        $(formId + " .btn-update-payment").show();
        //$(formId + " .tile-header").html("Edit Credit Card");
    },
    editBillingMethod: function (id, formId, zone) {
        $(formId + " #BillingFullName").val($(zone + " .billing-fullname-" + id).html());
        $(formId + " #BillingEmail").val($(zone + " #billing-email-" + id).val());
        $(formId + " #BillingCountry").val($(zone + " #billing-country-" + id).val());
        $(formId + " #BillingCity").val($(zone + " .billing-city-" + id).attr("value"));
        $(formId + " #BillingState").val($(zone + " #billing-state-" + id).val());
        $(formId + " #BillingZipCode").val($(zone + " #billing-zipcode-" + id).val());
        $(formId + " #BillingAddress1").val($(zone + " #billing-address1-" + id).val());
        $(formId + " #BillingAddress2").val($(zone + " #billing-address2-" + id).val());
        $(formId + " #BillingPhone").val($(zone + " #billing-phone-" + id).val());
        $(formId + " #PaymentInformationID").val(id);
    },
    addBillingMethod: function (id, formId, zone) {
        if (zone == ".profile-payment") {
            $(formId + " #BillingFullName").val("");
            $(formId + " #BillingEmail").val("");
            $(formId + " #BillingCountry").val("");
            $(formId + " #BillingCity").val("");
            $(formId + " #BillingState").val("");
            $(formId + " #BillingZipCode").val("");
            $(formId + " #BillingAddress1").val("");
            $(formId + " #BillingAddress2").val("");
            $(formId + " #BillingPhone").val("");
        }

        $(formId + " #PaymentInformationID").val(0);
    },
    actionPayment: function (zone) {

        $(zone + " .box-payment .payment-del").on("click", function () {
            var id = $(this).attr("payment-id");
            var memberId = $(this).attr("member-id");
            pinJs.delPaymentMethod(id, zone + " #box-payment-method", zone, false, memberId);
            $(".profile-payment #profile-plan").hide();
        });

        $(zone + " .box-payment .payment-edit").on("click", function () {
            var paymentMethodTypeID = $(this).attr("payment-method-typeid");
            if (paymentMethodTypeID != 2) {
                $(zone + " #payment-box-update").show();
                $(zone + " #billing-box-update").hide();
                $(zone + " #ach-payment-method").hide();
                var id = $(this).attr("payment-id");
                pinJs.editPaymentMethod(id, zone + " #frmPaymentMethod");
                $(zone + " #frmPaymentMethod").find("input").removeClass("input-validation-error");
                $(zone + " #frmPaymentMethod").find("select").removeClass("input-validation-error");
                $(zone + " #frmPaymentMethod").find("span.field-validation-error").html("");
                $(zone + " .group-close").hide();
                $(".profile-payment #profile-plan").hide();

                $(zone + " #chkSameAsHomeAddress").attr("checked", false);
            }
            else {
                $(zone + " #ach-payment-method").show();
                $(zone + " #billing-box-update").hide();
                $(zone + " #payment-box-update").hide();
                var id = $(this).attr("payment-id");
                pinJs.editAchPaymentMethod(id, zone + " #frmACHPaymentMethod");
                $(zone + " #frmACHPaymentMethod").find("input").removeClass("input-validation-error");
                $(zone + " #frmACHPaymentMethod").find("select").removeClass("input-validation-error");
                $(zone + " #frmACHPaymentMethod").find("span.field-validation-error").html("");
                $(zone + " .group-close").hide();
                $(".profile-payment #profile-plan").hide();

                $(zone + " #chkSameAsHomeAddress").attr("checked", false);
            }

        });
        $(".profile-payment #profile-plan").hide();
    },

    //fill for widget
    renderProfilePlanWidget: function (memberId) {
        $.ajax(
              {
                  url: '/Plan/ProfileRenderHtml/' + memberId,
                  type: "GET",
                  beforeSend: function (xhr, opts) {

                  },
                  success: function (data) {
                      $("#tz-form-singup").append(response);
                  }
              });
    },

    //for case form edit
    actionPayment2: function (zone, generateFormEdit) {
        $(zone + " .box-payment .payment-del").on("click", function () {
            var id = $(this).attr("payment-id");
            var memberId = $(this).attr("member-id");
            pinJs.delPaymentMethod(id, zone + " #box-payment-method", zone, true, memberId);
            $(".profile-payment #profile-plan").hide();
        });
        $(zone + " .box-payment .payment-edit").on("click", function () {
            //$(zone + " #payment-box-update").hide();
            //$(zone + " #billing-box-update").hide();
            var paymentMethodTypeID = $(this).attr("payment-method-typeid");
            if (paymentMethodTypeID != 2) {
                $(zone + " #billing-box-edit").hide();
                $(zone + " #payment-box-edit").show();
                $(zone + " #frmACHPaymentMethod" + generateFormEdit + " #ach-payment-method").hide();
                var id = $(this).attr("payment-id");
                pinJs.editPaymentMethod2(id, zone + " #frmPaymentMethodEdit");
                $(zone + " #frmPaymentMethodEdit").find("input").removeClass("input-validation-error");
                $(zone + " #frmPaymentMethod").find("select").removeClass("input-validation-error");
                $(zone + " #frmPaymentMethodEdit").find("span.field-validation-error").html("");
                $(zone + " .group-close").hide();
                $(".profile-payment #profile-plan").hide();

                $(zone + " #chkSameAsHomeAddress").attr("checked", false);
            } else {
                $(zone + " #frmACHPaymentMethod" + generateFormEdit + " #ach-payment-method").show();
                $(zone + " #billing-box-edit").hide();
                $(zone + " #payment-box-edit").hide();
                var id = $(this).attr("payment-id");
                pinJs.editAchPaymentMethod(id, zone + " #frmACHPaymentMethod" + generateFormEdit);
                $(zone + " #frmACHPaymentMethod" + generateFormEdit + " .tile-header-cc").html("Edit Payment Method");
                $(zone + " #frmACHPaymentMethod" + generateFormEdit).find("input").removeClass("input-validation-error");
                $(zone + " #frmACHPaymentMethod" + generateFormEdit).find("select").removeClass("input-validation-error");
                $(zone + " #frmACHPaymentMethod" + generateFormEdit).find("span.field-validation-error").html("");
                $(zone + " .group-close").hide();
                $(".profile-payment #profile-plan").hide();

                $(zone + " #frmACHPaymentMethod" + generateFormEdit + " #chkSameAsHomeAddress").attr("checked", false);
            }

        });
    },
    actionBillingAddress: function (zone) {
        $(zone + " .box-payment .billing-edit").on("click", function () {
            $(zone + " #payment-box-update").hide();
            $(zone + " #payment-box-edit").hide();
            $(zone + " #billing-box-update").show();
            $(zone + " #frmBillingInfo .tile-header").html("Edit Billing Address");
            var id = $(this).attr("billing-id");
            pinJs.editBillingMethod(id, zone + " #frmBillingInfo", zone);
            $(zone + " #frmBillingInfo").find("input").removeClass("input-validation-error");
            $(zone + " #frmBillingInfo").find("span.field-validation-error").html("");
            $(zone + " .group-close").hide();
            $(".profile-payment #profile-plan").hide();
        });
        $(zone + " .billing-add").on("click", function () {
            $(zone + " #payment-box-update").hide();
            $(zone + " #payment-box-edit").hide();
            $(zone + " #billing-box-update").show();
            $(zone + " #frmBillingInfo .tile-header").html("Add New Billing Address");
            var id = $(this).attr("billing-id");
            pinJs.addBillingMethod(id, zone + " #frmBillingInfo", zone);
            $(zone + " #frmBillingInfo").find("input").removeClass("input-validation-error");
            $(zone + " #frmBillingInfo").find("span.field-validation-error").html("");
            $(zone + " .group-close").hide();
            $(".profile-payment #profile-plan").hide();
        });
        $(zone + " .box-payment .billing-del").on("click", function () {
            var id = $(this).attr("billing-id");
            pinJs.delBillingAddress(id, zone + " #box-billing-info", zone);
            $(".profile-payment #profile-plan").hide();
        });
    },
    actionBillingAddress2: function (zone) {
        $(zone + " .box-payment .billing-edit").on("click", function () {
            //$(zone + " #payment-box-update").hide();
            //$(zone + " #billing-box-update").hide();
            $(zone + " #payment-box-edit").hide();
            $(zone + " #billing-box-edit").show();
            //$(zone + " #frmBillingInfo .tile-header").html("Edit Billing Address");
            var id = $(this).attr("billing-id");
            pinJs.editBillingMethod(id, zone + " #frmBillingInfoEdit", zone);
            $(zone + " #frmBillingInfoEdit").find("input").removeClass("input-validation-error");
            $(zone + " #frmBillingInfoEdit").find("span.field-validation-error").html("");
            $(".profile-payment #profile-plan").hide();
        });
        $(zone + " .billing-add").on("click", function () {
            $(zone + " #payment-box-update").hide();
            $(zone + " #billing-box-update").show();
            //$(zone + " #payment-box-edit").hide();            
            //$(zone + " #billing-box-edit").hide();
            $(zone + " #frmBillingInfo .tile-header").html("Add New Billing Address");
            var id = $(this).attr("billing-id");
            pinJs.addBillingMethod(id, zone + " #frmBillingInfo", zone);
            //$(zone + " #frmBillingInfo").find("input").removeClass("input-validation-error");
            //$(zone + " #frmBillingInfo").find("span.field-validation-error").html("");

            $(".profile-payment #profile-plan").hide();
        });
        $(zone + " .box-payment .billing-del").on("click", function () {
            var id = $(this).attr("billing-id");
            pinJs.delBillingAddress(id, zone + " #box-billing-info", zone, true);
            $(".profile-payment #profile-plan").hide();
        });
    },
    initFormPaymentPost: function (infoForm, zone, isEdit) {
        //handler post form        
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                    infoForm.find(".btn-save-payment").attr("disabled", "disabled");
                    infoForm.find(".btn-update-payment").attr("disabled", "disabled");
                    infoForm.find('.btn-cancel').attr("disabled", "disabled");
                    pinJs.blockUIForLoading();
                },
                success: function (data) {
                    infoForm.find(".btn-save-payment").removeAttr("disabled");
                    infoForm.find(".btn-update-payment").removeAttr("disabled");
                    infoForm.find('.btn-cancel').removeAttr("disabled");
                    if (data.Success == 1) {
                        $(zone + " .group-payment").removeClass("dsp-none");
                        $(zone + " .group-payment").show();
                        //ContentPaymentHTML
                        $(zone + " #box-payment-method").html(data.ContentPaymentHTML);
                        //pinJs.resetFormPaymentMethod(zone);                        
                        //re-init action
                        if (isEdit) {
                            pinJs.actionPayment2(zone);
                            //pinJs.resetFormPayment(zone, "#frmPaymentMethodEdit");
                            if (infoForm.selector == zone + " #frmPaymentMethod") {
                                $(zone + " #payment-box-update").hide();
                                $(zone + " #billing-box-update").hide();
                                pinJs.resetFormPayment(zone, "#frmPaymentMethod");
                            } else {
                                $(zone + " #payment-box-edit").hide();
                                $(zone + " #billing-box-edit").hide();
                                pinJs.resetFormPayment(zone, "#frmPaymentMethodEdit");
                            }
                        } else {
                            pinJs.actionPayment(zone);
                            $(zone + " #payment-box-update").hide();
                            $(zone + " #billing-box-update").hide();
                            pinJs.resetFormPayment(zone, "#frmPaymentMethod");
                        }

                        alertify.success(data.Message);
                        $(zone + " .group-close").show();
                    } else {
                        var form = " #frmPaymentMethodEdit";
                        if (infoForm.selector == zone + " #frmPaymentMethod") {
                            form = " #frmPaymentMethod";
                        }
                        //if (isEdit) form = " #frmPaymentMethodEdit";
                        if (data.Field && data.Field == "MonthExp") {
                            $(zone + form + " .box-payment-method #MonthExp").addClass("input-validation-error");
                            $(zone + form + " .box-payment-method #MonthExp").focus();
                        } else {
                            $(zone + form + " .box-payment-method #CardNumber").addClass("input-validation-error");
                            $(zone + form + " .box-payment-method #CardNumber").focus();
                        }
                        alertify.error(data.Message);
                    }
                    pinJs.unBlockUIForLoading();
                }
            });
        }
    },
    initFormACHPaymentPost: function (infoForm, zone, isEdit) {
        //handler post form        
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                    infoForm.find(".btn-ach-save-payment").attr("disabled", "disabled");
                    infoForm.find(".btn-ach-update-payment").attr("disabled", "disabled");
                    infoForm.find('.btn-cancel').attr("disabled", "disabled");
                    pinJs.blockUIForLoading();
                },
                success: function (data) {
                    infoForm.find(".btn-ach-save-payment").removeAttr("disabled");
                    infoForm.find(".btn-ach-update-payment").removeAttr("disabled");
                    infoForm.find('.btn-cancel').removeAttr("disabled");
                    if (data.Success == 1) {
                        $(zone + " .group-payment").removeClass("dsp-none");
                        $(zone + " .group-payment").show();
                        //ContentPaymentHTML
                        $(zone + " #box-payment-method").html(data.ContentPaymentHTML);
                        //pinJs.resetFormPaymentMethod(zone);                        
                        //re-init action
                        if (isEdit) {
                            pinJs.actionPayment2(zone);
                            //pinJs.resetFormPayment(zone, "#frmPaymentMethodEdit");
                            if (infoForm.selector == zone + " #frmACHPaymentMethod") {
                                $(zone + " #payment-box-update").hide();
                                $(zone + " #ach-payment-method").hide();
                                $(zone + " #billing-box-update").hide();
                                $(zone + " #frmACHPaymentMethod #PaymentMethodID").val("0");
                                $(zone + " #frmACHPaymentMethod #CheckName").val("");
                                $(zone + " #frmACHPaymentMethodEdit #CheckAba").val("");
                                $(zone + " #frmACHPaymentMethod #CheckAccount").val("");
                                $(zone + " #frmACHPaymentMethod #AccountHolderTypeID").val("1");
                                $(zone + " #frmACHPaymentMethod #AccountTypeID").val("");
                                $(zone + " #frmACHPaymentMethod #SecCode").val("");
                                $(zone + " #credit-card-ico").removeClass();
                            } else {
                                $(zone + " #payment-box-edit").hide();
                                $(zone + " #billing-box-edit").hide();
                                $(zone + " #frmACHPaymentMethod #PaymentMethodID").val("0");
                                $(zone + " #frmACHPaymentMethod #CheckName").val("");
                                $(zone + " #frmACHPaymentMethodEdit #CheckAba").val("");
                                $(zone + " #frmACHPaymentMethod #CheckAccount").val("");
                                $(zone + " #frmACHPaymentMethod #AccountHolderTypeID").val("1");
                                $(zone + " #frmACHPaymentMethod #AccountTypeID").val("");
                                $(zone + " #frmACHPaymentMethod #SecCode").val("");
                                $(zone + " #credit-card-ico").removeClass();
                            }
                        } else {
                            pinJs.actionPayment(zone);
                            $(zone + " #payment-box-update").hide();
                            $(zone + " #ach-payment-method").hide();
                            $(zone + " #billing-box-update").hide();
                            $(zone + " #frmACHPaymentMethod #PaymentMethodID").val("0");
                            $(zone + " #frmACHPaymentMethod #CheckName").val("");
                            $(zone + " #frmACHPaymentMethodEdit #CheckAba").val("");
                            $(zone + " #frmACHPaymentMethod #CheckAccount").val("");
                            $(zone + " #frmACHPaymentMethod #AccountHolderTypeID").val("1");
                            $(zone + " #frmACHPaymentMethod #AccountTypeID").val("");
                            $(zone + " #frmACHPaymentMethod #SecCode").val("");
                            $(zone + " #credit-card-ico").removeClass();
                        }

                        alertify.success(data.Message);
                        $(zone + " .group-close").show();
                    } else {
                        var form = " #frmPaymentMethodEdit";
                        if (infoForm.selector == zone + " #frmACHPaymentMethod") {
                            form = " #frmACHPaymentMethod";
                        }
                        //if (isEdit) form = " #frmPaymentMethodEdit";
                        //if (data.Field && data.Field == "MonthExp") {
                        //    $(zone + form + " .box-payment-method #MonthExp").addClass("input-validation-error");
                        //    $(zone + form + " .box-payment-method #MonthExp").focus();
                        //} else {
                        //    $(zone + form + " .box-payment-method #CardNumber").addClass("input-validation-error");
                        //    $(zone + form + " .box-payment-method #CardNumber").focus();
                        //}                        
                        alertify.error(data.Message);
                    }
                    pinJs.unBlockUIForLoading();
                }
            });
        }
    },
    initFormPaymentPostAddNew: function (infoForm, zone, isEdit) {
        ////handler button        
        //infoForm.find(".k-grid-update").on("click", function () {
        //    infoForm.submit();
        //});
        //handler post form        
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                    infoForm.find(".btn-save-payment").attr("disabled", "disabled");
                    infoForm.find('.btn-cancel').attr("disabled", "disabled");
                    pinJs.blockUIForLoading();
                },
                success: function (data) {
                    infoForm.find(".btn-save-payment").removeAttr("disabled");
                    infoForm.find('.btn-cancel').removeAttr("disabled");
                    if (data.Success == 1) {
                        //alertify.success(data.Message);
                        if ($("#frm-booking-store").val() != "undefined" && $("#frm-booking-store").val() != null && $("#frm-booking-store").val() > 0) {
                            var $el = $("#popBuyStoreItem #store-paymenttoken");
                            //$el.append($("<option></option>").attr("value", data.PaymentTokenID).text(data.CustomName));
                            $el.getKendoDropDownList().dataSource.add({ "CustomName": data.CardNumber, "Token": data.PaymentTokenID });

                            var options = {
                                id: 'popMemberPaymentInfo',
                                width: 750,
                                title: false
                            }
                            var popupWindow = pinJs.initPopupWindow(options);
                            popupWindow.close();
                        } else {
                            var $el = $("#frmAddOrEditBookingPlan #PaymentTokenID");
                            $el.append($("<option></option>").attr("value", data.PaymentTokenID).text(data.CardNumber));

                            var options = {
                                id: 'popMemberPaymentInfo',
                                width: 750,
                                title: false
                            }
                            var popupWindow = pinJs.initPopupWindow(options);
                            popupWindow.close();
                        }
                    } else {
                        alertify.error(data.Message);
                    }
                    pinJs.unBlockUIForLoading();
                }
            });
        }
    },

    initFormPaymentPostReCharge: function (infoForm) {
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
                {
                    url: formURL,
                    type: "POST",
                    data: postData,
                    beforeSend: function (xhr, opts) {
                        infoForm.find(".btn-save-payment-method-recharge").attr("disabled", "disabled");
                        infoForm.find('.btn-cancel').attr("disabled", "disabled");
                        pinJs.blockUIForLoading();
                    },
                    success: function (data) {
                        infoForm.find(".btn-save-payment-method-recharge").removeAttr("disabled");
                        infoForm.find('.btn-cancel').removeAttr("disabled");
                        if (data.Success == 1) {
                            alertify.success(data.Message);
                            var options = {
                                id: "popupPaymentInfoReCharge",
                                width: 900,
                                title: false,
                                draggable: false,
                                resizable: false
                            };
                            var popupWindow = pinJs.initPopupWindow(options);
                            if (popupWindow) popupWindow.close();
                            var gridData = $('#PaymentReportingList').data("kendoGrid");
                            if (gridData) gridData.dataSource.read();
                        } else if (data.Success == 0) {
                            alertify.error(data.Message);
                        }
                        else {
                            validator.showErrors(data.Errors);
                        }
                        pinJs.unBlockUIForLoading();
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        infoForm.find(".btn-save-payment-method-recharge").removeAttr("disabled");
                        infoForm.find('.btn-cancel').removeAttr("disabled");
                        console.error(jqXHR);
                        console.error(textStatus);
                        console.error(errorThrown);
                        pinJs.unBlockUIForLoading();
                    }
                });
        }
    },

    initFormAchPaymentPostAddNew: function (infoForm, zone, isEdit) {
        ////handler button        
        //infoForm.find(".k-grid-update").on("click", function () {
        //    infoForm.submit();
        //});
        //handler post form        
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                    infoForm.find(".btn-save-payment").attr("disabled", "disabled");
                    infoForm.find('.btn-cancel').attr("disabled", "disabled");
                    pinJs.blockUIForLoading();
                },
                success: function (data) {
                    infoForm.find(".btn-save-payment").removeAttr("disabled");
                    infoForm.find('.btn-cancel').removeAttr("disabled");
                    if (data.Success == 1) {
                        //alertify.success(data.Message);
                        if ($("#frm-booking-store").val() != "undefined" && $("#frm-booking-store").val() != null && $("#frm-booking-store").val() > 0) {
                            var $el = $("#popBuyStoreItem #store-paymenttoken");
                            //$el.append($("<option></option>").attr("value", data.PaymentTokenID).text(data.CustomName));
                            $el.getKendoDropDownList().dataSource.add({ "CustomName": data.CustomName, "Token": data.PaymentTokenID });

                            var options = {
                                id: 'popMemberPaymentInfo',
                                width: 750,
                                title: false
                            }
                            var popupWindow = pinJs.initPopupWindow(options);
                            popupWindow.close();
                        } else {
                            var $el = $("#frmAddOrEditBookingPlan #PaymentTokenID");
                            $el.append($("<option></option>").attr("value", data.PaymentTokenID).text(data.CustomName));

                            var options = {
                                id: 'popMemberPaymentInfo',
                                width: 750,
                                title: false
                            }
                            var popupWindow = pinJs.initPopupWindow(options);
                            popupWindow.close();
                        }

                    } else {
                        alertify.error(data.Message);
                    }
                    pinJs.unBlockUIForLoading();
                }
            });
        }
    },
    initFormBillingPost: function (infoForm, zone, isEdit) {
        //handler post form
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                success: function (data) {
                    if (data.Success == 1) {
                        $(zone + " .group-billing").removeClass("dsp-none");
                        $(zone + " .group-billing").show();
                        $(zone + " #box-billing-info").html(data.ContentBillingHTML);
                        //pinJs.resetFormPaymentMethod(zone);
                        //re-init action 
                        if (isEdit) {
                            pinJs.actionBillingAddress2(zone);
                            if (infoForm.selector == zone + " #frmBillingInfo") {
                                $(zone + " #payment-box-update").hide();
                                $(zone + " #billing-box-update").hide();
                                pinJs.resetFormBilling(zone, "#frmBillingInfo");
                            } else {
                                $(zone + " #payment-box-edit").hide();
                                $(zone + " #billing-box-edit").hide();
                                pinJs.resetFormBilling(zone, "#frmBillingInfoEdit");
                            }
                        } else {
                            pinJs.actionBillingAddress(zone);
                            pinJs.resetFormBilling(zone, "#frmBillingInfo");
                            $(zone + " #payment-box-update").hide();
                            $(zone + " #billing-box-update").hide();
                        }

                        alertify.success(data.Message);
                        $(zone + " .group-close").show();
                    } else {
                        alertify.error(data.Message);
                    }
                }
            });
        }
    },
    initFormPaymentDepositPost: function (infoForm, zone, isEdit) {
        //handler post form        
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                    infoForm.find(".btn-update-payment-deposit").attr("disabled", "disabled");
                },
                success: function (data) {
                    infoForm.find(".btn-update-payment-deposit").removeAttr("disabled");
                    if (data.Success === true) {
                        alertify.success(data.Message);
                    } else {
                        alertify.error(data.Message);
                    }
                }
            });
        }
    },
    initFormMerchantApplicationPost: function (infoForm, zone, isEdit) {
        //handler post form        
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            infoForm.find(".btn-update-member-applicationID").attr("disabled", "disabled");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                async: false,
                data: postData,
                success: function (data) {
                    infoForm.find(".btn-update-member-applicationID").removeAttr("disabled");
                    if (data.Success === true) {
                        alertify.success(data.Message);
                        infoForm.find(".btn-delete").attr("data-id", data.MerchantId);
                        infoForm.find("#MemberApplicationID").val(data.MerchantId);
                        infoForm.find("#StatusID").val(data.StatusID);
                    } else {
                        alertify.error(data.Message);
                    }
                },
                error: function () {
                    infoForm.find(".btn-update-member-applicationID").removeAttr("disabled");
                }
            });
        }
    },
    initFormRoomPost: function (infoForm, zone, isEdit) {
        //handler post form
        $.validator.unobtrusive.parse(infoForm);
        var validator = infoForm.data("validator");
        validator.settings.ignore = ':hidden, [readonly=readonly], [src]';
        validator.settings.submitHandler = function (form) {
            var postData = $(form).serialize();
            var formURL = $(form).attr("action");
            $.ajax(
            {
                url: formURL,
                type: "POST",
                data: postData,
                beforeSend: function (xhr, opts) {
                },
                success: function (data) {
                    if (data.Success == 1) {
                        $(zone + " .group-list-room").removeClass("hide");
                        $(zone + " .group-list-room").show();
                        $(zone + " .box-update-room").hide();
                        $(zone + " #list-romm").html(data.ContentHTML);
                        alertify.success(data.Message);
                    } else {
                        alertify.error(data.Message);
                    }
                }
            });
        }
    },
    roomTurnOnOff: function (infoForm, zone, _valueOnOff) {
        $.ajax(
            {
                url: '/ProfileInformation/TurnOnOffRoom',
                type: "POST",
                data: {
                    valueOnOff: _valueOnOff
                },
                beforeSend: function (xhr, opts) {
                },
                success: function (data) {
                    if (data.Success == 1) {
                        $(zone + " .turn-off").removeClass("hide");
                        $(zone + " .turn-on").removeClass("hide");
                        if (_valueOnOff == "1") {
                            $(zone + " .turn-on").hide();
                            $(zone + " .turn-off").show();
                            $(zone + " #StatusID").val(1);
                        } else {
                            $(zone + " .turn-on").show();
                            $(zone + " .turn-off").hide();
                            $(zone + " #StatusID").val(0);
                        }

                        $(zone + " .group-list-room").removeClass("hide");
                        $(zone + " .group-list-room").show();
                        $(zone + " .box-update-room").hide();
                        $(zone + " #list-romm").html(data.ContentHTML);
                        alertify.success(data.Message);
                    } else {
                        alertify.error(data.Message);
                    }
                }
            });
    },
    editRoom: function (id, formId) {
        $(formId + " #Name").val($(".room-name-" + id).attr("value"));
        $(formId + " #Description").val($("#room-desc-" + id).attr("value"));
        $(formId + " #RoomID").val($("#room-id-" + id).attr("value"));
        //sunv hotfix on/off by room 2016-09-15
        var statusId = $("#room-statusid-" + id).attr("value");

        if (statusId == "1") {
            $(formId + ' #chkStatusID').bootstrapSwitch('state', true);
        } else {
            $(formId + ' #chkStatusID').bootstrapSwitch('state', false);
        }
        $(formId).find("input").removeClass("input-validation-error");
    },
    resetFormRoom: function (formId) {
        $(formId + " #RoomID").val("0");
        $(formId + " #Description").val("");
        $(formId + " #Name").val("");
        //$(formId + ' #chkStatusID').bootstrapSwitch('state', true);
        $(formId).find("input").removeClass("input-validation-error");
        $(formId).find("span.field-validation-error").html("");
    },
    resetFormPaymentMethod: function (zone) {
        $(zone + " #frmPaymentMethod #PaymentMethodID").val("0");
        //$(zone + " .box-payment-method .field-validation-error").html("");
        //$(zone + " .box-payment-method").find("input").removeClass("input-validation-error");
        //$(zone + " .box-payment-method").find("select").removeClass("input-validation-error");
        $(zone + " #frmPaymentMethod #CardNumber").val("");
        $(zone + " #frmPaymentMethodEdit #CardNumber").val("");
        $(zone + " #frmPaymentMethod #frmPaymentMethod").val("0");
        $(zone + " #frmPaymentMethod #CardType").val("");
        $(zone + " #frmPaymentMethod #MonthExp").val("");
        $(zone + " #frmPaymentMethod #YearExp").val("");
        $(zone + " #frmPaymentMethodEdit #MonthExp").val("");
        $(zone + " #frmPaymentMethodEdit #YearExp").val("");
        $(zone + " #frmPaymentMethod #FullName").val("");
        $(zone + " #frmPaymentMethod #Cvv").val("");
        $(zone + " #frmPaymentMethod #Token").val("");
        $(zone + " #frmPaymentMethodEdit #Token").val("");
        //$(zone + " #payment-box-update").hide();
        //$(zone + " #billing-box-update").hide();
        //$(zone + " #payment-box-edit").hide();
        //$(zone + " #billing-box-edit").hide();
        $(zone + " #credit-card-ico").removeClass();

        //reset billing address
        $(zone + " #frmPaymentMethod #BillingFullName").val("");
        $(zone + " #frmPaymentMethod #BillingEmail").val("");
        $(zone + " #frmPaymentMethod #BillingCountry").val("");
        $(zone + " #frmPaymentMethod #BillingCity").val("");
        $(zone + " #frmPaymentMethod #BillingState").val("");
        $(zone + " #frmPaymentMethod #BillingZipCode").val("");
        $(zone + " #frmPaymentMethod #BillingAddress1").val("");
        $(zone + " #frmPaymentMethod #BillingAddress2").val("");
        $(zone + " #frmPaymentMethod #BillingPhone").val("");

        $(zone + ' #frmPaymentMethod #chkSameAsHomeAddress').attr('checked', false);
    },

    resetFormACHPaymentMethod: function (zone) {
        $(zone + " #frmACHPaymentMethod #PaymentMethodID").val("0");
        $(zone + " #frmACHPaymentMethod #CheckName").val("");
        $(zone + " #frmACHPaymentMethod #CheckAba").val("");
        $(zone + " #frmACHPaymentMethod #CheckAccount").val("");
        $(zone + " #frmACHPaymentMethod #AccountHolderTypeID").val("1");
        $(zone + " #frmACHPaymentMethod #AccountTypeID").val("");
        $(zone + " #frmACHPaymentMethod #SecCode").val("");
        $(zone + " #credit-card-ico").removeClass();

        //reset billing address
        $(zone + " #frmACHPaymentMethod #BillingFullName").val("");
        $(zone + " #frmACHPaymentMethod #BillingEmail").val("");
        $(zone + " #frmACHPaymentMethod #BillingCountry").val("");
        $(zone + " #frmACHPaymentMethod #BillingCity").val("");
        $(zone + " #frmACHPaymentMethod #BillingState").val("");
        $(zone + " #frmACHPaymentMethod #BillingZipCode").val("");
        $(zone + " #frmACHPaymentMethod #BillingAddress1").val("");
        $(zone + " #frmACHPaymentMethod #BillingAddress2").val("");
        $(zone + " #frmACHPaymentMethod #BillingPhone").val("");

        $(zone + ' #frmACHPaymentMethod #chkSameAsHomeAddress').attr('checked', false);
    },

    resetFormPayment: function (zone, formId) {
        $(zone + " " + formId + " #PaymentMethodID").val("0");
        $(zone + " " + formId + " #CardNumber").val("");
        $(zone + " " + formId + " #FullName").val("0");
        $(zone + " " + formId + " #CardType").val("");
        $(zone + " " + formId + " #MonthExp").val("");
        $(zone + " " + formId + " #YearExp").val("");
        $(zone + " " + formId + " #MonthExp").val("");
        $(zone + " " + formId + " #YearExp").val("");
        $(zone + " " + formId + " #FullName").val("");
        $(zone + " " + formId + " #Cvv").val("");
        $(zone + " " + formId + " #Token").val("");
        $(zone + " " + formId + " #credit-card-ico").removeClass();
    },
    resetFormPaymentMethodAdd: function (zone) {
        $(zone + " #frmPaymentMethod #PaymentMethodID").val("0");
        //$(zone + " .box-payment-method .field-validation-error").html("");
        //$(zone + " .box-payment-method").find("input").removeClass("input-validation-error");
        //$(zone + " .box-payment-method").find("select").removeClass("input-validation-error");
        $(zone + " #frmPaymentMethod #CardNumber").val("");
        $(zone + " #frmPaymentMethod #frmPaymentMethod").val("0");
        $(zone + " #frmPaymentMethod #CardType").val("");
        $(zone + " #frmPaymentMethod #MonthExp").val("");
        $(zone + " #frmPaymentMethod #YearExp").val("");
        $(zone + " #frmPaymentMethod #FullName").val("");
        $(zone + " #frmPaymentMethod #Cvv").val("");
        $(zone + " #frmPaymentMethod #Token").val("");
        //$(zone + " #payment-box-update").hide();
        //$(zone + " #billing-box-update").hide();
        //$(zone + " #payment-box-edit").hide();
        //$(zone + " #billing-box-edit").hide();
        $(zone + " #credit-card-ico").removeClass();
    },
    resetFormBilling: function (zone, formId) {
        $(zone + " " + formId + " .field-validation-error").html("");
        $(zone + " " + formId).find("input").removeClass("input-validation-error");
        $(zone + " " + formId).find("select").removeClass("input-validation-error");
        $(zone + " " + formId + " #PaymentInformationID").val("0");
        $(zone + " " + formId + " #BillingFullName").val("");
        $(zone + " " + formId + " #BillingEmail").val("");
        $(zone + " " + formId + " #BillingCountry").val("");
        $(zone + " " + formId + " #BillingCity").val("");
        $(zone + " " + formId + " #BillingState").val("");
        $(zone + " " + formId + " #BillingZipCode").val("");
        $(zone + " " + formId + " #BillingAddress1").val("");
        $(zone + " " + formId + " #BillingAddress2").val("");
        $(zone + " " + formId + " #BillingPhone").val("");
    },
    initFormPaymentMethodAdd: function (zone) {
        $(zone + " #frmPaymentMethod #PaymentMethodID").val("0");
        $(zone + " .box-payment-method .field-validation-error").html("");
        $(zone + " .box-payment-method").find("input").removeClass("input-validation-error");
        $(zone + " .box-payment-method").find("select").removeClass("input-validation-error");
    },
    openPopupPaymentInfo: function (memberId) {
        var options = {
            id: "popMemberPaymentInfo",
            width: 650,
            title: false,
            draggable: false,
            resizable: false
        };
        var popupWindow = pinJs.initPopupWindow(options);
        popupWindow.refresh({
            url: "/Payment/PopupPaymentMethod/" + memberId,
            width: 765,
            async: false
        }).center().open();
        $('#popMemberPaymentInfo').addClass('scroll');
        $("#popMemberPaymentInfo").parent().addClass("mobileModal");
        //init form
        //pinJs.initFormWindow("frmPaymentMethod", popupWindow);
    },
    openPopupAttendanceReportInfo: function () {
        var options = {
            id: "popAttendanceReportInfo",
            width: 450,
            title: false,
            draggable: false,
            resizable: false
        };
        var popupWindow = pinJs.initPopupWindow(options);
        popupWindow.refresh({
            url: "/Calendar/AttendanceReportForm/",
            async: false
        }).center().open();
        $('#popAttendanceReportInfo').addClass('scroll');
        $("#popAttendanceReportInfo").parent().addClass("mobileModal");
        //init form
        //pinJs.initFormWindow("frmPaymentMethod", popupWindow);
    },
    dateToHoursAMPM: function (date) {
        var hours = date.getHours();
        var minutes = date.getMinutes();
        var ampm = hours >= 12 ? 'pm' : 'am';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes < 10 ? '0' + minutes : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return strTime;
    },
    builtAttendanceReport: function (obj, isSchool, isTeacher, isStudent) {
        var pageTo = "javascript:void(0)";
        var host = location.protocol + "//" + location.hostname + (window.location.port ? ":" + window.location.port : "");
        if (isSchool) {
            pageTo = host + "/Calendar/AttendanceReport?ID=" + obj.TeacherID + "&From=" + obj.StartDate + "&To=" + obj.EndDate;
        }
        if (isTeacher) {
            pageTo = host + "/Calendar/AttendanceReport?&From=" + obj.StartDate + "&To=" + obj.EndDate;
        }
        if (isStudent) {
            var id = "";
            if (obj.StudentID > 0) {
                id = "&" + "ID=" + obj.StudentID;
            }
            pageTo = host + "/Calendar/AttendanceReportStudent?&From=" + obj.StartDate + "&To=" + obj.EndDate + id;
        }
        window.location.replace(pageTo);
        //$.ajax({
        //    url: '/Calendar/AttendanceReportParam',
        //    type: "POST",
        //    dataType: "json",
        //    //async: false,
        //    data: {
        //        model: obj
        //    },
        //    success: function (response) {
        //        if (response.Success == 1) {
        //            var pageTo = location.protocol + "//" + location.hostname + (window.location.port ? ":" + window.location.port : "") + "/Calendar/AttendanceReport?ID=" + obj.TeacherID + "&From=" + obj.StartDate + "&To=" + obj.EndDate;
        //            window.location.replace(pageTo);
        //        } else {
        //            window.pinJs.msgError(response.ErrorMsg);
        //        }
        //    },
        //    error: function (xhr, ajaxOptions, thrownError) {                    
        //    }
        //});
    },    
    loadExtTemplate: function (path) {
        //http://docs.kendoui.com/howto/load-templates-external-files
        //Use jQuery Ajax to fetch the template file
        var tmplLoader = $.get(path)
            .success(function (result) {
                //On success, Add templates to DOM (assumes file only has template definitions)
                $("body").append(result);
            })
            .error(function (result) {
                alertify.error("Error Loading Templates -- TODO: Better Error Handling");
            });

        tmplLoader.complete(function () {
            //Publish an event that indicates when a template is done loading
            $(host).trigger("TEMPLATE_LOADED", [path]);
        });
    },
    //--------------Message alert--------------------
    alert: function (options) {
        var titleHeader = "";
        if (typeof options.titleHeader != undefined) {
            titleHeader = options.titleHeader;
        }
        var defaultOptions = {
            layout: 'center',
            theme: 'defaultTheme',
            type: 'alert',
            text: '',
            dismissQueue: true,
            template: '<div class="noty_message"><div class="noty-header" >' + titleHeader + '</div><div class="noty_text"></div><div class="noty_close"></div></div>',
            animation: {
                open: { height: 'toggle' },
                close: { height: 'toggle' },
                easing: 'swing',
                speed: 500
            },
            timeout: 5000, //1s
            force: false,
            modal: true,
            maxVisible: 5, // max item display
            closeWith: ['button'], /// ['click', 'button', 'hover']
            callback: {
                onShow: function () {
                },
                afterShow: function () {
                    var that = this;
                    $.each(that.options.buttons, function (i, v) {
                        if (v.focus != undefined && v.focus == true) {
                            $(that.$buttons).find("button")[i].focus();
                            return false;
                        }
                    });
                },
                onClose: function () {
                },
                afterClose: function () {
                },
                onCloseClick: function () {
                }
            },
            buttons: false
        };
        /* merge options into defaultOptions, recursively */
        $.extend(true, defaultOptions, options);


        if (defaultOptions.type == 'success') {
            defaultOptions.callback.onClose.call();
            this.log(defaultOptions.text);
        } else {
            if (defaultOptions.type == 'showsuccess') {
                defaultOptions.type = 'success';
            }

            return noty(defaultOptions);
        }
        //return noty(defaultOptions);
    },
    msgAlert: function (options) {
        var settings = $.extend({}, { type: 'alert' }, options);
        this.alert(settings);
    },
    msgSuccess: function (options) {
        var settings = $.extend({}, { type: 'success' }, options);
        this.alert(settings);
    },
    msgError: function (options, input) {
        var settings = {
            type: 'error',
            buttons: [
                {
                    //addClass: 'btn btn-primary',
                    addClass: 'btn btn-grey btn-crm btn-ok',
                    text: 'OK',
                    onClick: function ($noty) {
                        // this = button element
                        // $noty = $noty element
                        if (input != undefined) {
                            if (input.attr("type") == 'text')
                                input.focus();
                            else
                                input.siblings("input:visible").focus();

                            // return false;
                        }
                        $noty.close();
                    },
                    focus: true
                }
            ],
            modal: true,
            titleHeader: 'Error',
            text: options
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    msgWarning: function (options) {
        var settings = {
            type: 'warning',
            buttons: [
                {
                    //addClass: 'btn btn-primary',
                    addClass: 'btn btn-grey btn-crm btn-ok',
                    text: 'OK',
                    onClick: function ($noty) {
                        // this = button element
                        // $noty = $noty element
                        $noty.close();
                    },
                    focus: true
                }
            ],
            modal: true,
            titleHeader: 'Warning'
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    msgInfor: function (options) {
        var settings = $.extend({}, { type: 'information' }, options);
        this.alert(settings);
    },
    msgShowSuccess: function (options) {
        var settings = {
            type: 'showsuccess',
            titleHeader: 'Success'
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },

    msgConfirm: function (options) {
        var settings = {
            type: 'confirm',
            closeWith: ['button'],
            buttons: [
                {
                    //addClass: 'btn btn-primary',
                    addClass: 'btn btn-primary btn-crm btn-ok',
                    text: 'Yes',
                    onClick: function ($noty) {
                        // this = button element
                        // $noty = $noty element
                        $noty.close();
                    },
                    focus: false

                }, {
                    //addClass: 'btn btn-danger',
                    addClass: 'btn btn-primary btn-crm btn-cancel',
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    },
                    focus: true
                }
            ],
            modal: true,
            titleHeader: 'Confirmation'
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    msgWarningWithAbort: function (options) {
        var settings = {
            type: 'confirm',
            buttons: [
                {
                    addClass: 'btn btn-primary btn-cancel',
                    text: 'Abort',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ],
            modal: true
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    //--------------End Message alert--------------------
    log: function (text) {
        if (typeof window.console != 'undefined') {

        }
    },
    l: function (name) {
        try {
            var lang = "en";
            if (arguments.length > 1) {
                lang = arguments[1];
            }
            return (eval("pinJs.localize.{0}.{1}".format(lang, name)));
        } catch (err) {
            //alertify.error(err.message);
            return "Miss key language: " + name;
        }
    },
    ajax: function (options) {
        var defaultOptions = {
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            //async: false,
            cache: false,
            success: function (result) {
            }
        };
        $.extend(true, defaultOptions, options);
        $.ajax(defaultOptions);
    },
    AddAntiForgeryToken: function (data, elForm) {
        //data.__RequestVerificationToken = $(elForm + '#__AjaxAntiForgeryForm input[name=__RequestVerificationToken]').val();
        if (typeof elForm !== 'undefined') {
            elForm += " ";
        }
        data.__RequestVerificationToken = $(elForm + 'input[name=__RequestVerificationToken]').val();
        return JSON.stringify(data);
    },
    formatPhoneNumber: function (phoneNumber) {
        var piece1 = phoneNumber.substring(0, 3); //123
        var piece2 = phoneNumber.substring(3, 6); //456
        var piece3 = phoneNumber.substring(6); //7890

        //should return (123)456-7890
        return kendo.format("({0})-{1}-{2}", piece1, piece2, piece3);
    },
    ValidateDateTime: function (dateTime) {
        //only format mm/DD/YYYY H:MM PM
        var isValidDate = false;
        try {
            var arr1 = dateTime.split('/');
            var year = 0;
            var month = 0;
            var day = 0;
            var hour = 0;
            var minute = 0;
            var sec = 0;
            if (arr1.length == 3) {
                var arr2 = arr1[2].split(' ');
                if (arr2.length == 3) {
                    var arr3 = arr2[1].split(':');
                    year = parseInt(arr2[0], 10);
                    month = parseInt(arr1[0], 10);
                    day = parseInt(arr1[1], 10);
                    hour = parseInt(arr3[0], 10);
                    minute = parseInt(arr3[1], 10);
                    //sec = parseInt(arr3[0],10);
                    sec = 0;

                    var ampm = arr2[2];
                    if (isNaN(year) || isNaN(month) || isNaN(day) || isNaN(hour) || isNaN(minute) || !(ampm == 'AM' || ampm == 'PM'))
                        return false;

                    var isValidTime = false;
                    if (hour >= 0 && hour <= 12 && minute >= 0 && minute <= 59 && sec >= 0 && sec <= 59)
                        isValidTime = true;
                    else if (hour == 12 && minute == 0 && sec == 0)
                        isValidTime = true;

                    if (isValidTime) {
                        var isLeapYear = false;
                        if (year % 4 == 0)
                            isLeapYear = true;

                        if ((month == 4 || month == 6 || month == 9 || month == 11) && (day >= 0 && day <= 30))
                            isValidDate = true;
                        else if ((month != 2) && (day >= 0 && day <= 31))
                            isValidDate = true;

                        if (!isValidDate) {
                            if (isLeapYear) {
                                if (month == 2 && (day >= 0 && day <= 29))
                                    isValidDate = true;
                            } else {
                                if (month == 2 && (day >= 0 && day <= 28))
                                    isValidDate = true;
                            }
                        }
                    }
                    if (year <= 0 || (month <= 0 || month > 12) || day <= 0) {
                        isValidDate = false;
                    }
                }

            }
        } catch (err) {
            isValidDate = false;
        }
        return isValidDate;

    },
    ValidateDate: function (date) {
        //only format mm/DD/YYYY
        var isValidDate = false;
        try {
            var arr1 = date.split('/');
            var year = 0;
            var month = 0;
            var day = 0;
            if (arr1.length == 3) {
                year = parseInt(arr1[2], 10);
                month = parseInt(arr1[0], 10);
                day = parseInt(arr1[1], 10);

                if (isNaN(year) || isNaN(month) || isNaN(day))
                    return false;

                var isLeapYear = false;
                if (year % 4 == 0)
                    isLeapYear = true;

                if ((month == 4 || month == 6 || month == 9 || month == 11) && (day >= 0 && day <= 30))
                    isValidDate = true;
                else if ((month != 2) && (day >= 0 && day <= 31))
                    isValidDate = true;

                if (!isValidDate) {
                    if (isLeapYear) {
                        if (month == 2 && (day >= 0 && day <= 29))
                            isValidDate = true;
                    } else {
                        if (month == 2 && (day >= 0 && day <= 28))
                            isValidDate = true;
                    }
                }

                if (year <= 0 || (month <= 0 || month > 12) || day <= 0) {
                    isValidDate = false;
                }

            }
        } catch (err) {
            isValidDate = false;
        }
        return isValidDate;
    },
    ValidateTime: function (time) {
        //only format H:MM PM
        var isValidTime = false;
        try {
            var hour = 0;
            var minute = 0;
            var sec = 0;
            var arr1 = time.split(' ');
            if (arr1.length == 2) {
                var arr2 = arr1[0].split(':');
                hour = parseInt(arr2[0], 10);
                minute = parseInt(arr2[1], 10);
                //sec = parseInt(arr3[0],10);
                sec = 0;
                var ampm = arr1[1];
                if (isNaN(hour) || isNaN(minute) || !(ampm == 'AM' || ampm == 'PM'))
                    return false;

                if (hour >= 0 && hour <= 12 && minute >= 0 && minute <= 59 && sec >= 0 && sec <= 59)
                    isValidTime = true;
                else if (hour == 12 && minute == 0 && sec == 0)
                    isValidTime = true;

            }
        } catch (err) {
            isValidTime = false;
        }
        return isValidTime;
    },
    //validate date with format
    isDate: function (txtDate, formatDate) {
        if (formatDate == undefined) formatDate = 'mm/dd/yyyy';
        formatDate = formatDate.toLowerCase();
        var currVal = txtDate;
        if (currVal == '')
            return false;

        var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
        var dtArray = currVal.match(rxDatePattern); // is format OK?

        if (dtArray == null)
            return false;

        //Checks for format.
        switch (formatDate) {
            case "mm/dd/yyyy":
                dtMonth = dtArray[1];
                dtDay = dtArray[3];
                dtYear = dtArray[5];
                break;
            case "dd/mm/yyyy":
                dtMonth = dtArray[3];
                dtDay = dtArray[1];
                dtYear = dtArray[5];
                break;
            case "yyyy/mm/dd":
                dtMonth = dtArray[3];
                dtDay = dtArray[5];
                dtYear = dtArray[1];
                break;
        }
        if (dtMonth < 1 || dtMonth > 12)
            return false;
        else if (dtDay < 1 || dtDay > 31)
            return false;
        else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
            return false;
        else if (dtMonth == 2) {
            var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
            if (dtDay > 29 || (dtDay == 29 && !isleap))
                return false;
        }
        return true;
    },
    ValidateDecimal: function (value, key) {
        // using for validating inline edit Money valua
        var msg = "";
        if (!($.isNumeric(value))) {
            msg = window.pinJs.l("Validate_Invalid").format(window.pinJs.l(key));
            return msg;
        } else {
            var vla = parseFloat(value);
            if (vla == undefined || vla.toString() !== value.toString()) {
                return window.pinJs.l("Message_NumberLarger").format(window.pinJs.l(key));
            }
            if (vla < 0) {
                msg = window.pinJs.l("Validate_BiggerThan").format(window.pinJs.l(key), 0);
                return msg;
            }
        }
        return msg;
    },
    ValidateInteger: function (value, key) {
        // using for validating inline edit Integer valua
        var msg = "";
        if (!($.isNumeric(value))) {
            msg = window.pinJs.l("Validate_Invalid").format(window.pinJs.l(key));
            return msg;
        } else {
            var vla = parseInt(value);
            if (vla == undefined || vla.toString() !== value.toString()) {
                return window.pinJs.l("Message_NumberLarger").format(window.pinJs.l(key));
            }
            if (vla < 0) {
                msg = window.pinJs.l("Validate_BiggerThan").format(window.pinJs.l(key), 0);
                return msg;
            }
        }
        return msg;
    },
    ValidateEmail: function (email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
    },
    FindTopmostElement: function (elm) {
        //Find top most kendo window opened: .k-window:visible
        var idx = 0;
        var that = $(elm);
        if (that.length <= 1)
            return that;
        that.each(function (k, v) {
            var value = $(v).css('zIndex');
            if ($.inArray(value, ['inherit', 'auto']) == -1 && parseInt(value) > idx)
                idx = k - 1;
        });
        return that.eq(idx);
    },
    HandleEnterKeyPress: function () {
        $("body").keydown(function (e) {
            if (e.which == 13) {
                //catch enter on focus kendo container on opened or textarea, a, button nothing to do
                if ($('.k-animation-container:visible').length > 0 || $("*:focus").is("textarea, button, a, .k-item")) return;

                var topWindow = window.pinJs.FindTopmostElement('.k-window:visible');
                if (topWindow.length == 1) {
                    var submitButton = topWindow.find('.form-submit-button:visible:enabled, .btn-save:visible:enabled, .btn-create:visible:enabled, .btn-edit:visible:enabled').first();
                    if (submitButton.length == 1 && submitButton.is(':focus') == false) {

                        submitButton.click();
                    }
                }

                //var submitButton = $(".form-submit-button:visible:enabled").last();
                //if (submitButton.length == 1) {
                //    if (submitButton.is(":focus") == false)
                //        submitButton.click();
                //} else {

                //    return;
                //    submitButton = $(".k-window .btn-save:visible:enabled, .k-window .btn-create:visible:enabled").last(); //.btn-crm-submit:enabled,
                //    if (submitButton.length == 1 && submitButton.is(":focus") == false) {
                //        submitButton.click();
                //    }
                //} //else nothing to do
            }
        });
    },
    SubStr: function (input, count) {
        try {
            var flg = true;
            var arr = input.split(' ');
            var strSub = "";
            $.each(arr, function () {
                if ((strSub.length + this.length) < count) {
                    strSub += this;
                    strSub += " ";
                } else {
                    if (flg) {
                        strSub += "...";
                        flg = false;
                    }
                }
            });
        } catch (e) {
        }
        return strSub;
    },
    setValueDefaultObject: function (obj) {
        if (typeof obj == "object") {
            for (var i in obj) {
                switch (typeof obj[i]) {
                    case "object":
                        if (obj[i] == null) {
                            obj[i] = "";
                        }
                        break;
                    case "string":
                        if (obj[i].length == 0) {
                            obj[i] = "";
                        }
                        break;
                    case "number":
                        if (obj[i].length == 0) {
                            obj[i] = 0;
                        }
                        break;
                }
            }
        }
        return obj;
    },
    //Url
    getUrlVars: function () {
        //http://jquery-howto.blogspot.com/2009/09/get-url-parameters-values-with-jquery.html
        var vars = [], hash;
        var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
        for (var i = 0; i < hashes.length; i++) {
            hash = hashes[i].split('=');
            vars.push(hash[0]);
            vars[hash[0]] = hash[1];
        }
        return vars;
    },
    getUrlVar: function (name) {
        return this.getUrlVars()[name];
    },
    autoRedirect: function () {
        //using call if have  returnUrl window.pinJs.autoRedirect() or window.pinJs.autoRedirect("param_url")
        var url = window.pinJs.getUrlVar('returnUrl');
        if (arguments.length == 1) {
            url = arguments[0];
        }
        if (url != 'undefined') {
            window.location.href = url;
        }
    },
    goBack: function () {
        if (pinJs.getReturnUrl() != undefined) {
            window.location.href = unescape(pinJs.getReturnUrl());
        } else if (window.location.href.indexOf('returnList=True') != -1) {
            //window.history.go(-2); //Go back list
            window.location.href = pinJs.oGlobal.RootURL + pinJs.oGlobal.controller + '/Index';
        } else {
            if (window.history.length == 1)
                window.location.href = document.referrer;
            else
                window.history.go(-1);
        }
        return false;
    },
    goForward: function () {
        window.history.go(+1);
        return false;
    },
    getReturnUrl: function () {
        return pinJs.getUrlVar('returnUrl');
    },
    //End Url
    generateFilterCharacter: function (elGrid, param) {
        var t = this;
        if (typeof (param) == 'object') {
            param.unshift('#');
            param.unshift('all');
            var $keyFilter = $('.left-filter-by-charactor');
            for (var i = 0; i < param.length; i++) {
                var elAppend = $("<li class='title'>{0}</li>".format((param[i]).toUpperCase()))
                    .on('click', function () {
                        t.filterCharacter(elGrid, $(this).text().toLowerCase());
                    });
                $keyFilter.append(elAppend);
            }
            $(elGrid).append($keyFilter);

        }
    },
    filterCharacter: function (elGrid, value) {
        var grid = $(elGrid).data("kendoGrid");
        var initialFilter = { logic: "and", filters: [] };
        if (value != '' && value != 'all') {
            if ($('#ModuleLayout').length > 0) {
                var moduleLayout = $('#ModuleLayout').data('kendoComboBox').value();
                var filterModule = {
                    logic: "and",
                    filters: [
                        { field: "KeyFilter", operator: "eq", value: value },
                        { field: "ModuleID", operator: "eq", value: moduleLayout },
                        { field: "keyword", operator: "contains", value: $("#search-layout-input").val() }
                    ]
                };
                if (moduleLayout == 0) {
                    filterModule = {
                        logic: "and",
                        filters: [
                            { field: "KeyFilter", operator: "eq", value: value },
                            { field: "keyword", operator: "contains", value: $("#search-layout-input").val() }
                        ]
                    };
                }
                initialFilter.filters.push(filterModule);
                grid.dataSource.filter(initialFilter);
            } else {
                grid.dataSource.filter({ field: "KeyFilter", operator: "eq", value: value });
            }
        } else {
            if ($('#ModuleLayout').length > 0) {
                if ($('#ModuleLayout').data('kendoComboBox') != undefined) {
                    var moduleLayout = $('#ModuleLayout').data('kendoComboBox').value();
                    var filterModule = {
                        logic: "and",
                        filters: [
                            { field: "ModuleID", operator: "eq", value: moduleLayout },
                            { field: "keyword", operator: "contains", value: $("#search-layout-input").val() }
                        ]
                    };
                    if (moduleLayout == 0) {
                        filterModule = {
                            logic: "and",
                            filters: [
                                { field: "keyword", operator: "contains", value: $("#search-layout-input").val() }
                            ]
                        };
                    }
                    initialFilter.filters.push(filterModule);
                    grid.dataSource.filter(initialFilter);
                }
            } else {
                $("#searchFlag").val(0);
                $("#inputSearchGrid").val('');
                //$("#inputSearchGrid").animate({ "width": "0px" }, "slow");
                //setTimeout(function () {
                //    $("#inputSearchGrid").addClass("hidden");
                //}, 600);
                grid.dataSource.filter([]);
            }
        }
    },
    //function for quick search on all main module
    quickSearchListView: function ($btnSearch, $inputSearch, $searchFlag, $gridSearch) {
        var searchFlag = '0';
        $btnSearch.on("click", function () {
            searchFlag = $searchFlag.val();
            if (searchFlag == '0') {
                $inputSearch.removeClass("hidden");
                //$inputSearch.animate({ "width": "350px" }, "slow");
                setTimeout(function () {
                    $inputSearch.focus();
                }, 600);
                $searchFlag.val(1);
            }
            //else {
            if ($searchFlag.val() != '') {
                $gridSearch.data("kendoGrid").dataSource.page(1);
            }

            if ($inputSearch.val() == '') {
                $inputSearch.removeClass("hidden");
                $searchFlag.val(0);
            }
            //}
        });
        $inputSearch.keypress(function (e) {
            if (e.which == 13) {
                $gridSearch.data("kendoGrid").dataSource.page(1);
            }
        });
    },
    //function for quick search on instrument
    quickSearchListViewInstrument: function ($btnSearch, $inputSearch, $instructor, $searchFlag, $gridSearch) {
        var searchFlag = '0';
        $btnSearch.on("click", function () {
            searchFlag = $searchFlag.val();
            if (searchFlag == '0') {
                $inputSearch.removeClass("hidden");
                setTimeout(function () {
                    $inputSearch.focus();
                }, 600);
                $searchFlag.val(1);
            }
            //else {
            if ($searchFlag.val() != '') {
                $gridSearch.data("kendoGrid").dataSource.page(1);
            }

            if ($inputSearch.val() == '') {
                $inputSearch.removeClass("hidden");
                $searchFlag.val(0);
            }
            //}
        });
        $inputSearch.keypress(function (e) {
            if (e.which == 13) {
                $gridSearch.data("kendoGrid").dataSource.page(1);
            }
        });
        $instructor.keypress(function (e) {
            if (e.which == 13) {
                $gridSearch.data("kendoGrid").dataSource.page(1);
            }
        });
    },
    //filter for course
    filterCourse: function ($btnSearch, $inputSearch, $searchFlag, $gridSearch, $instrument, $song, $artist, $genre, $level, $instructor, $tag) {
        var searchFlag = '0';
        var grid = $("#CourseList").data("kendoGrid");
        $btnSearch.off('click');
        $btnSearch.on("click", function () {
            searchFlag = $searchFlag.val();
            if (searchFlag == '0') {
                $inputSearch.removeClass("hidden");
                setTimeout(function () {
                    $inputSearch.focus();
                }, 600);
                $searchFlag.val(1);
            }
            //else {
            if ($searchFlag.val() != '') {
                $gridSearch.data("kendoGrid").dataSource.page(1);
                if ($("#hidCourseTypeId").val() == $("#hidCourseTypeSongID").val()) {
                    grid.showColumn("Artist");
                    grid.showColumn("SongTitle");
                    grid.hideColumn("Title");
                    tooltip("allcourse-table", 2);
                } else {
                    grid.hideColumn("Artist");
                    grid.hideColumn("SongTitle");
                    grid.showColumn("Title");
                    tooltip("allcourse-table", 1);
                }
            }

            if ($inputSearch.val() == '') {
                $inputSearch.removeClass("hidden");
                $searchFlag.val(0);
            }
            //}
        });
        $inputSearch.keypress(function (e) {
            if (e.which == 13) {
                $gridSearch.data("kendoGrid").dataSource.page(1);
                if ($("#hidCourseTypeId").val() == $("#hidCourseTypeSongID").val()) {
                    grid.showColumn("Artist");
                    grid.showColumn("SongTitle");
                    grid.hideColumn("Title");
                } else {
                    grid.hideColumn("Artist");
                    grid.hideColumn("SongTitle");
                    grid.showColumn("Title");
                }
            }
        });
    },
    //filter for All Video & MemberReport
    filterAllVideo: function ($btnSearch, $inputSearch, $searchFlag, $gridSearch, $updatedDate) {
        var searchFlag = '0';
        $btnSearch.on("click", function () {
            searchFlag = $searchFlag.val();
            if (searchFlag == '0') {
                $inputSearch.removeClass("hidden");
                setTimeout(function () {
                    $inputSearch.focus();
                }, 600);
                $searchFlag.val(1);
            }
            //else {
            if ($searchFlag.val() != '') {
                $gridSearch.data("kendoGrid").dataSource.page(1);
            }

            if ($inputSearch.val() == '') {
                $inputSearch.removeClass("hidden");
                $searchFlag.val(0);
            }
            //}
        });
        $inputSearch.keypress(function (e) {
            if (e.which == 13) {
                $gridSearch.data("kendoGrid").dataSource.page(1);
            }
        });
    },
    //function for quick search on Members, Contact
    quickSearchListViewMember: function ($btnSearch, $inputFirstName, $inputMail, $inputKeyword, $searchFlag, $gridSearch) {
        var searchFlag = '0';
        $btnSearch.on("click", function () {
            if ($searchFlag.val() != '') {
                $gridSearch.data("kendoGrid").dataSource.page(1);
                return false;
            }
        });
        if ($inputFirstName != "") {
            $inputFirstName.keypress(function (e) {
                if (e.which == 13) {
                    $btnSearch.click();
                    $gridSearch.data("kendoGrid").dataSource.page(1);
                    return false;
                }
            });
        }
        if ($inputMail != "") {
            $inputMail.keypress(function (e) {
                if (e.which == 13) {
                    $btnSearch.click();
                    $gridSearch.data("kendoGrid").dataSource.page(1);
                    return false;
                }
            });
        }
        if ($inputKeyword != "") {
            $inputKeyword.keypress(function (e) {
                if (e.which == 13) {
                    $btnSearch.click();
                    $gridSearch.data("kendoGrid").dataSource.page(1);
                    return false;
                }
            });
        }
    },
    quickChangeListViewMember: function ($gridSearch) {
        $gridSearch.data('kendoGrid').dataSource.bind('change', function () {
            pinJs.addLessonToListMember();
        });
    },
    kendoComboboxHasData: function (cbb) {
        /// validate item not found in combobox
        var selectedIndex = cbb.data("kendoComboBox").select();
        if (selectedIndex >= 0) {
            return true;
        }
        return false;
    },
    kendoComboBoxOnChange: function () {
        // Using for kendoCombox sugesstion input
        // If data has not Input value, reset value of combobox
        var selectedIndex = this.select();
        if (selectedIndex < 0) {
            this.value(null);
            this.dataSource.filter([]);
        }
    },
    kendoComboBoxOnChangeMember: function () {
        // Using for kendoCombox sugesstion input
        // If data has not Input value, reset value of combobox
        var selectedIndex = this.select();
        if (selectedIndex < 0) {
            this.value(null);
            this.dataSource.filter([]);
        }
        memberController.changeBirthDay();
    },
    kendoMultiComboBoxOnChangeSchool: function () {
        //Get teacher by school
        var lst = [];
        var listSchool = $("#frmAddOrEditMember #ListSchoolID").data("kendoMultiSelect").dataItems();
        for (var i = 0; i < listSchool.length; i++) {
            lst.push(listSchool[i].Value);
        }
        $.ajax({
            url: '/Member/GetTecherBySchool',
            type: "POST",
            dataType: "json",
            data: {
                listSchool: lst
            },
            success: function (response) {
                if (response.StatusID == 1 || response.StatusID == 0) {
                    var dataSource = new kendo.data.DataSource({
                        data: JSON.parse(response.Data)
                    });
                    if ($('#ListTeacherID').length > 0) {
                        var multiselect = $('#ListTeacherID').data("kendoMultiSelect");
                        multiselect.setDataSource(dataSource);
                    }
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
            }
        });
        //}        
    },
    kendoComboBoxOnDataBound: function () {
        if (!this.firstload && this.select() < 0) {
            this.firstload = true;
            this.value(null);
        }
    },
    kendoComboBoxOnChangeInvalid: function (cb) {
        // Using for kendoCombox sugesstion input
        // If data has not Input value, reset value of combobox
        var selectedIndex = cb.select();
        if (selectedIndex < 0) {
            cb.value(null);
        }
    },
    kendoNumberBoxOnChange: function (e) {
        if (this.value() == null) {
            this.value(0);
        }
    },
    kendoNumberBoxOnChangeMaxValue: function (e) {
        var value = this.value();
        if (value == null) {
            this.value(0);
        } else {
            if (value < this.min() || value >= this.max()) {
                this.value(this.oldvalue);
            }
        }
    },
    kendoDatetimePickerOnChange: function (e) {
        var date = Date.parse(this.value());
    },
    getDateFromString: function (str) { // format date : mm/dd/yyyy
        var parts = str.split('/');
        var date = new Date(parseInt(parts[2], 10), // year
            parseInt(parts[0], 10) - 1, // month, starts with 0
            parseInt(parts[1], 10));
        return date;
    },
    getDateTimeFromString: function (str) { // format date : mm/dd/yyyy hh:mm
        var dateRegex = /^([0]\d|[1][0-2])\/([0-2]\d|[3][0-1])\/([2][01]|[1][6-9])\d{2}(\s([0-1]\d|[2][0-3])(\:[0-5]\d){1,2})?$/;
        if (str.match(dateRegex) != null)
            return new Date(str);
        return null;

        //var dateSp = str.split(' ')[0];
        //var timeSp = str.split(' ')[1];
        //var parts = dateSp.split('/');
        //var times = timeSp.split(':');
        //var date = new Date(parseInt(parts[2], 10), // year
        //    parseInt(parts[0], 10) - 1, // month, starts with 0
        //    parseInt(parts[1], 10),
        //    parseInt(times[0], 10),
        //    parseInt(times[1], 10)
        //);
        //return date;
    },

    gotoDetail: function (link) {
        if (link != null && link.length > 0) {
            $(window).attr("location", link);
        }
    },

    getCRMCookie: function (cname) {
        var name = cname + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i].trim();
            if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
        }
        return "";
    },
    setCRMCookie: function (cname, cvalue, exdays) {
        var d = new Date();
        d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
        var expires = "expires=" + d.toGMTString();
        document.cookie = cname + "=" + cvalue + "; " + expires + "; path=/";
    },
    deleteCRMCookie: function (cname) {
        var currentSearchId = window.pinJs.getCRMCookie(cname);
        if (currentSearchId != "") {
            document.cookie = cname + "=; expires=Thu, 01 Jan 1970 00:00:00 GMT; path=/";
        }
    },
    convertToRealNumberFormat: function (number, format) {
        var twoDigit = number.substring(number.indexOf('.') + 1, number.length);
        var intNumber = Number(number.substring(0, number.indexOf('.')));
        var intNumberFormat = kendoToStringFormat(intNumber, format);
        var result = intNumberFormat.substring(0, intNumberFormat.indexOf('.') + 1) + twoDigit;
        return result;
    },
    validateExceptSpecialCharacter: function (text) {
        //return /[\<\>\!\@\#\$\%\^\&\*\(\)\'\"\;\`]/.test(text);
        return /[\<\>\!\@\#\$\%\^\&\(\)\'\"\;\`]/.test(text);
    },
    find_duplicates: function (arr) {
        var len = arr.length,
            out = [],
            counts = {};

        for (var i = 0; i < len; i++) {
            var item = arr[i];
            counts[item] = counts[item] >= 1 ? counts[item] + 1 : 1;
        }

        for (var item in counts) {
            if (counts[item] > 1)
                out.push(item);
        }

        return out;
    },
    /*****************Hash URL*****************/
    JsScript: {
        loadResource: function (pathJs, callbackInit) {
            //http://unixpapa.com/js/dyna.html

            var head = document.getElementsByTagName('head')[0];
            var script = document.createElement('script');
            script.type = 'text/javascript';
            if (typeof callbackInit != "undefined") {
                script.onreadystatechange = function () {
                    //if (this.readyState == 'complete') helper();
                    if (this.readyState == 'complete') {
                        window.pinJs.initController(callbackInit);
                    }
                };
            }

            //script.onload = helper;
            //script.src = 'helper.js';
            script.src = pathJs;
            head.appendChild(script);
        }
    },
    //File
    FileSystem: {
        getExtension: function (file) {
            var extension = file.substr((file.lastIndexOf('.') + 1));
            return extension;
        }
    },
    //new project
    GenerateId: function () {
        return (new Date()).getTime() * -1;
    },
    genUniqueId: function () {

    },
    changeNavArrow: function (element) {
        if ($(element).hasClass('disSubs')) {
            $('#nav-student').attr('class', 'collapsed');
            $('#nav-member').attr('class', 'collapsed');
            $('#nav-learning').attr('class', 'collapsed');
            $(element).removeClass('disSubs')
        } else {
            $('#nav-student').attr('class', '');
            $('#nav-member').attr('class', '');
            $('#nav-learning').attr('class', '');
            $(element).addClass('disSubs')
        }
    },
    refreshtable: function (id) {
        var oGrid = $("#" + id + "");
        oGrid.data("kendoGrid").dataSource.page(1);
    },
    addLessonToListMember: function () {
        setTimeout(function () {
            var a = $('#MemberGrid td a.grid-mem-name');
            var eleStatus = $('#MemberGrid td a.grid-mem-status');
            a.parent().addClass('email');
            $.each(a, function (index) {
                var memId = $(this).attr('data-id');
                $.get('/Member/LessonUser' + '?id=' + memId, function (data) {
                    $(a[index]).parent().append(data);
                    WebRtcDemo.UpdateUserList(memId);
                });
            });
            $.each(eleStatus, function (index) {
                var memId = $(this).attr('data-id');
                $.get('/Member/LessonStatusUser' + '?id=' + memId, function (data) {
                    $(eleStatus[index]).parent().append(data);
                });
            });
        }, 1000);
    },
    countUnreadMessage: function (type) {
        $.get('/Message/GetCountUnread' + '?type=' + type, function (data) {
            if (data && data > 0) {
                $('#link-tab-message-inbox').html('Inbox <span class="badge">' + data + '</span>');
            } else {
                $('#link-tab-message-inbox').html('Inbox');
            }
        });
    },
    replaceOneItem: function () {
        setTimeout(function () {
            var a = $('div.k-pager-wrap span.k-pager-info');
            if (a && a.text() == '1 - 1 of 1 items') {
                a.text('1 - 1 of 1 item');
            }
            $('[data-toggle="popover"]').popover({
                container: 'body',
                //content: 'Hello world',
                //trigger: 'focus',
                html: true,
                //animation: false,
                placement: 'left'
            });

            $(".popover .popup-item-menu button.popup-close").on("click", function () {
                $(".popover .popup-item-menu").remove();
            });

        }, 1000);
    },
    replaceOneItemAllCourse: function () {
        setTimeout(function () {
            var a = $('div.k-pager-wrap span.k-pager-info');
            if (a && a.text() == '1 - 1 of 1 items') {
                a.text('1 - 1 of 1 item');
            }
            var pos = 'right';
            if ($(window).width() < 568) {
                pos = 'left';
            }
            $('[data-toggle="popover"]').popover({
                container: 'body',
                //content: 'Hello world',
                //trigger: 'focus',
                html: true,
                //animation: false,
                placement: pos
            });

            $(".popover .popup-item-menu button.popup-close").on("click", function () {
                $(".popover .popup-item-menu").remove();
            });

        }, 1000);
    },
    replaceOneItemPractice: function () {
        setTimeout(function () {
            var a = $('div.k-pager-wrap span.k-pager-info');
            if (a && a.text() == '1 - 1 of 1 items') {
                a.text('1 - 1 of 1 item');
            }
            var pos = 'left';
            $('[data-toggle="popover"]').popover({
                container: 'body',
                //content: 'Hello world',
                //trigger: 'focus',
                html: true,
                //animation: false,
                placement: pos
            });

            $(".popover .popup-item-menu button.popup-close").on("click", function () {
                $(".popover .popup-item-menu").remove();
            });

        }, 1000);
    },
    replaceOneItemYelp: function () {
        //show export
        var grid = $("#BusinessList").data("kendoGrid");
        if (grid.dataSource.total() > 0) {
            $('#hidTotalResult').val(grid.dataSource.total());
            $("#link-export-csv").show();
        } else {
            $("#link-export-csv").hide();
        }
    },
    setChecked: function (e) {
        var lst = $("#hidFileSelected").val().split(",");
        var i;
        for (i = 0; i < lst.length; ++i) {
            $("input[coursefileid=" + lst[i] + "]").prop("checked", true);
        }

        //set check all
        if ($('.file-item:checked').length == $('.file-item').length) {
            $('.select-all-file').prop('checked', true);
            $("#hidFileSelectedAll").val(1);
        } else {
            $('.select-all-file').prop('checked', false);
        }

        if (e.sender && e.sender._data.length > 0) {
            $.each(e.sender._data, function (i, v) {
                Uploader.checkHiddenEditGridMultiFile(v.UploaderToken);
            });
        }
    },
    JsMessage: {
        validateFileSize: "Do not upload file with size more than 10 MB",
        validateFileType: "Please choose file type JPG, GIF or PNG",
        updateBrowser: "Sorry! You are using an older or unsupported browser. Please update your browser",
        multilFile: "Though it is possible to upload multiple files at once, this demonstration does not allowed to do so to demonstrate pause and resume in a simple manner. Sorry :-(",
        notAllowUpload: "Sorry you are not allowed to upload",
        uploadFail: "Upload failed, try again in ",
        congratz: "Congratz, upload is completed now",
        extension: "Invalid File. Valid extensions are:\n\n",
        size: "Only accept file size lower than 10GB.",
        commentValidateRequired: "Comment field is required.",
        commentValidateLenth: "Comment field can not exceed 500 characters in length.",
        eventNameValidateRequired: "<label class=" + 'red-text' + ">The Event Name field is required.</label>",
        eventNameValidateLenth: "<label class=" + 'red-text' + ">The Event Name field can not exceed 100 characters in length.</label>",
        eventVenueValidateRequired: "<label class=" + 'red-text' + ">The Event Venue field is required.</label>",
        eventVenueValidateLenth: "<label class=" + 'red-text' + ">The Event Venue field can not exceed 255 characters in length.</label>",
        pamamentCredit: "<span for='AddtionalCredit' class=''>Quantity must be greater than 0</span>",
        pamamentOverCredit: "<span for='AddtionalCredit' class=''>Quantity must be less than 1000</span>",
        paymentCheckOutFirstName: "First Name is required",
        paymentCheckOutLastName: "Last Name is required",
        paymentCheckOutAddress: "Address is required",
        paymentCheckOutZipcode: "Zipcode is required",
        paymentCheckOutCity: "City is required",
        paymentCheckOutPhone: "Phone is required",
        paymentCheckOutNameCard: "Card Name invalid",
        paymentCheckOutCardNumber: "Card Number invalid",
        paymentCheckOutExpDate: "Card Expiry invalid",
        paymentCheckOutCvv: "Security code invalid",
        paymentCheckOutValidPhone: "Phone Number invalid",
        paymentCheckOutSupportCard: "Not supported card",
        paymentCheckOutValidZipcode: "Zipcode invalid",
        noMixInAlbum: "You do not choose any mixes for your album",
        noMixInPartyPlayList: "You do not choose any mixes for your party play list",
        mixitemAvaiable: "This items has been added in shopping cart",
        maxMixItem: "Do not allow more than 20 items in shopping cart",
        noMixAvaiable: "You have no mix in available list. Please purchase mixes and try again later"
    },
    imgError: function (image) {
        image.onerror = "";
        image.src = "/Assets/img/icon-user-default.png";
        return true;
    },
    checkAssignmentReminder: function () {
        if (($('#Reminder_OnceTime').val() == '' && $('#Reminder_OnceTime').attr('disabled') != 'disabled')
            || ($('#select-day').val() == '' && $('#select-day').attr('disabled') != 'disabled')
            || ($('#Reminder_EveryTime').val() == '' && $('#Reminder_EveryTime').attr('disabled') != 'disabled')) {
            if ($('#Reminder_OnceTime').val() == '' && $('#Reminder_OnceTime').attr('disabled') != 'disabled') {
                $('#Reminder_OnceTime').parent().find('.datetime-error').removeClass('hidden');
            } else {
                $('#Reminder_OnceTime').parent().find('.datetime-error').addClass('hidden');
            }
            if ($('#select-day').val() == '' && $('#select-day').attr('disabled') != 'disabled') {
                $('#select-day').parent().find('.selectday-error').removeClass('hidden');
            } else {
                $('#select-day').parent().find('.selectday-error').addClass('hidden');
            }
            if ($('#Reminder_EveryTime').val() == '' && $('#Reminder_EveryTime').attr('disabled') != 'disabled') {
                $('#Reminder_EveryTime').parent().find('.datetime-error').removeClass('hidden');
            } else {
                $('#Reminder_EveryTime').parent().find('.datetime-error').addClass('hidden');
            }
            return false;
        } else {
            $('#Reminder_EveryTime').parent().find('.datetime-error').addClass('hidden');
            $('#select-day').parent().find('.selectday-error').addClass('hidden');
            $('#Reminder_OnceTime').parent().find('.datetime-error').addClass('hidden');
            return true;
        }
    },
    imgLibraryError: function (image) {
        image.onerror = "";
        image.src = "/Assets/img/noimage.png";
        return true;
    },
    subInboxCountMess: function (messId) {
        $.each($('.IsUnRead label.myCheckbox input'), function () {
            var a = $(this).val();
            if (a == messId && !$(this).prop('checked') && $('#memberViewID').val() <= 0) {
                $(this).prop('checked', true);
            }
        });
        $.get("/Message/GetCountUnread?type=" + $('#hidTypeId').val(), function (data) {
            var c = $('#link-tab-message-inbox span.badge');
            if (c) {
                if (data && data > 0) {
                    $('#link-tab-message-inbox').html('Inbox <span class="badge">' + data + '</span>');
                } else {
                    $('#link-tab-message-inbox').html('Inbox');
                }
            }
        });
    },
    urlEncode: function (value) {
        return encodeURIComponent(value).
            replace(/['()]/g, escape). // i.e., %27 %28 %29
            replace(/\*/g, '%2A').
            replace(/%(?:7C|60|5E)/g, unescape);
    },
    urlDecode: function (value) {
        return decodeURIComponent((value + '').replace(/\+/g, '%20'));
    },
    //Added by SuNV 2015/09/08
    htmlEncode: function (value) {
        //create a in-memory div, set it's inner text(which jQuery automatically encodes)
        //then grab the encoded contents back out.  The div never exists on the page.
        return $('<div/>').text(value).html();
    },
    htmlDecode: function (value) {
        return $('<div/>').html(value).text();
    },
    replaceAll: function (find, replace, str) {
        return str.replace(new RegExp(find, 'g'), replace);
    },
    activeMenu: function (id) {
        $(id).addClass('active');
    },
    activeSubMenu: function (submenu, mainmenu) {
        $(submenu).addClass('active');
        $(mainmenu).addClass('active');
    },
    validEditorKendo: function (idEditor, editor_max_length) {
        var element = $('#' + idEditor);
        if (pinJs.replaceAll('&nbsp;', '', pinJs.htmlDecode(element.val())).trim() == '') {
            element.parent().children('iframe').addClass('input-validation-error-kendo');
            element.parent().children('iframe').removeClass('valid');
            $.each($('span.field-validation-valid'), function () {
                if ($(this).attr('data-valmsg-for') == idEditor) {
                    $(this).html('<span for="' + idEditor + '" class="">The ' + idEditor + ' field is required.</span>');
                    $(this).removeClass('field-validation-valid').addClass('field-validation-error');
                    element.parent().children('iframe').focus();
                }
            });
            return false;
        }
        //if (element.val().length > editor_max_length) {
        //    element.parent().children('iframe').removeClass('valid');
        //    element.parent().children('iframe').addClass('input-validation-error-kendo');
        //    $.each($('span.field-validation-valid'), function () {
        //        if ($(this).attr('data-valmsg-for') == idEditor) {
        //            $(this).html('<span for="' + idEditor + '" class="">The field ' + idEditor + ' must be a string with a maximum length of ' + editor_max_length + '.</span>');
        //            $(this).removeClass('field-validation-valid').addClass('field-validation-error');
        //            element.parent().children('iframe').focus();
        //        }
        //    });
        //    return false;
        //}
        return true;
    },
    SortListUser: function (elementUl) {
        var mylist = elementUl;
        var listitems = mylist.children('div.item-user-chat').get();
        listitems.sort(function (a, b) {
            var compA = $(a).find('span.countunread').text();
            var compB = $(b).find('span.countunread').text();
            var comparer = (compA && compA > compB) ? -1 : (compB && compA < compB) ? 1 : 0;
            return comparer;
        });
        listitems.sort(function (a, b) {
            var compA = $(a).find('span.time-history').text();
            var compB = $(b).find('span.time-history').text();
            var comparer = (compA == 'Today' && compB == 'Yesterday' ? -1 : (compB == 'Today' && compA == 'Yesterday' ? 1 : (compA > compB ? -1 : (compB > compA ? 1 : 0))));
            return comparer;
        });
        $.each(listitems, function (idx, itm) { mylist.append(itm); });
    },
    validEmail: function (s) {
        var expr = /^([\w-\.]+)@@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(s);
    },
    openAndFocusMemberOnChatBox: function (memberId) {
        $("#studenta-act-link-msg-hidden").click();
        var findContact = false;
        $.each($('#list-user-recent .item-user-chat'), function () {
            var data_mid = $(this).attr('data-mid');
            if (data_mid && data_mid == memberId) {
                window.LiveChat.ChatClick($(this));
                findContact = true;
            }
        });
        if (!findContact) {
            $.each($('#list-user .item-user-chat'), function () {
                var data_mid = $(this).attr('data-mid');
                if (data_mid && data_mid == memberId) {
                    var chat = $(this).clone();
                    $("#list-user-recent .mCSB_container").prepend(chat);
                    LiveChat.initForItemUserChat();
                    window.LiveChat.ChatClick($("#list-user-recent").find(chat));
                    $("#tab-control-chat #list-member-recent").click();
                    findContact = true;
                }
            });
        }
        if (!findContact) {
            $.ajax({
                url: "/MessageChat/GetContactInfor",
                type: "GET",
                data: { id: memberId, type: "send" },
                dataType: 'Json',
                success: function (data) {
                    if (data.Success) {
                        $("#list-user-recent .mCSB_container").prepend(data.Content);
                        $("#tab-control-chat #list-member-recent").click();
                        window.LiveChat.ChatClick($("#list-user-recent .item-user-chat:first"));
                        LiveChat.initForItemUserChat();
                    }
                }
            });
        }
        $("#list-user").mCustomScrollbar("scrollTo", "div.item-user-chat.active");
    },
    openPopupInvite: function (email) {
        window.inviteController = {
            ui: {
                formId: '#frmUpdate'
            }
        };
        var ui = inviteController.ui;
        var options = {
            id: "popupInvite",
            title: false
            //width: 875,
            //minHeight: 450
        }
        var popupWindow = pinJs.initPopupWindow(options);
        var url = '/Invite/Invite/';
        if (email && email != '') {
            url = '/Invite/Invite/?email=' + email;
        }
        popupWindow.refresh({
            url: url,
            async: false
        }).center().open();
        //init form
        pinJs.initFormWindow(ui.formId, popupWindow);
        $("#popupInvite").parent().addClass("mobileModal");
    },
    confirmLogout: function (e) {
        window.pinJs.msgConfirm({
            text: "Are you sure that you want to sign out?",
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        window.location.href = "/Member/Logout";
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },
    logintoStudent: function (userName) {
        var url = '/Member/SignInByUserName';

        if (Uploader.arrUploaderFile.length > 0) {
            var confirmLeavePage = confirm(Uploader.onBeforeUnloadText);

            if (confirmLeavePage) {
                Uploader.arrUploaderFile = [];
                $.post(url, { userName: userName }, function (data) {
                    if (data == "OK") {
                        var pageTo = location.protocol + "//" + location.hostname + (window.location.port ? ":" + window.location.port : "") + "/Dashboard";
                        window.location.replace(pageTo);
                    } else {
                        alertify.error(data);
                    }
                });
            }
        } else {
            $.post(url, { userName: userName }, function (data) {
                if (data == "OK") {
                    var pageTo = location.protocol + "//" + location.hostname + (window.location.port ? ":" + window.location.port : "") + "/Dashboard";
                    window.location.replace(pageTo);
                } else {
                    alertify.error(data);
                }
            });
        }


    },
    thumbnailError: function (image) {
        image.onerror = "";
        image.src = "/Assets/NewUI/images/no_thumbnail.jpg";
        return true;
    },
    updateListAfterAddEdit: function (idList, countHidden, countTotal, itemPerPage, btnLoadMore, response) {
        idList.prepend(response.Item);
        //total
        var total = countHidden.val();
        if (total == "") total = 0;
        countTotal.html(parseInt(total) + 1);
        countHidden.val(parseInt(total) + 1);
        if (parseInt(countHidden.val()) > itemPerPage) {
            btnLoadMore.parent().removeClass('hidden');
            idList.children('div').last().remove();
        }
    },
    updateListMemberAfterAddEdit: function (idList, countHidden, countTotal, itemPerPage, btnLoadMore, response) {
        //total        
        var total = countHidden.val();
        var page = 1;
        if ($('#member-page').val()) {
            page = $('#member-page').val() - 1;
        }
        if (total == "") total = 0;
        if (parseInt(countHidden.val()) < itemPerPage * page) {
            idList.append(response.Item);
        } else if (parseInt(countHidden.val()) >= itemPerPage * page) {
            if (btnLoadMore) {
                btnLoadMore.parent().removeClass('hidden');
            }
        }
        countTotal.html(parseInt(total) + 1);
        countHidden.val(parseInt(total) + 1);
        SbAdmin.initForOverlay();
        WebRtcDemo.UpdateUserList();
        DashboardAdmin.initButton();
    },
    activeMenuNew: function (liClass) {
        //menu
        $("#side-menu").find("li").removeClass("active");
        $("#side-menu " + liClass).find("ul.nav-second-level").removeClass("hide");
        pinJs.activeMenu("#side-menu " + liClass);
    },
    activeSubMenuNew: function (liClass, subLiClass) {
        //menu
        $("#side-menu").find("li").removeClass("active");
        $("#side-menu " + liClass).find("ul.nav-second-level").removeClass("hide");
        $("#side-menu " + liClass).find(".nav-second-level").addClass("in");
        $("#side-menu " + liClass).find(".nav-second-level").css("height", "auto");
        pinJs.activeSubMenu("#side-menu .nav-second-level " + subLiClass, "#side-menu " + liClass);
    },
    filterMemberInGroup: function (groupId, clsname, keyword) {
        $('#' + groupId).find('.' + clsname).each(function () {
            keyword = keyword.toLowerCase();
            var name = $(this).html().toLowerCase();
            if (name.indexOf(keyword) == -1) {
                $(this).parent().parent().parent().addClass('hide');
            } else {
                $(this).parent().parent().parent().removeClass('hide');
            }
        });
    },
    searchGroupStudents: function (contentId, seemoreId, page, pageSize, keyword) {
        //            $('#tmp-group-filter-by-name').val($('#group-filter-by-name').val());
        $.get('/Member/GetListGroup?page=' + page + '&pageSize=' + pageSize + '&keyword=' + keyword, function (result) {
            if (result && result.trim() != '') {
                if ($(".no-data").length == 0) {
                    $('#' + contentId).html(result);
                } else if ($(".group-member-after").length > 0) {

                    $('#' + contentId).empty();
                    var content =
                        '<div class="homework-item-box">'
                            + '<div class="row list-group-student" id="list-group-student" page="1" page-size="4">'
                            + result
                            + '</div>'
                            + '</div>';
                    $(".group-member-after").after(content);
                    $(".no-data").remove();
                } else {
                    $('#' + contentId).html(result);
                }

                var count = $("input#group-count-hidden");

                var total = count.val();
                if (total == undefined) total = 0;
                total = parseInt(total);

                $("a#group-count").text(total);
                if (total < page * pageSize) {
                    $('#' + seemoreId).parent().addClass('hidden');
                } else {
                    $('#' + seemoreId).parent().removeClass('hidden');
                }
                SbAdmin.initForOverlay();
                WebRtcDemo.UpdateUserList();
            } else {
                $('#' + seemoreId).parent().addClass('hidden');
            }
            GroupDetail.memberGroupAction();
        });
    },
    check_collapse: function () {
        pinJs.changeArrowTitle('course-desc', 'active', '<span class="hide-on-mobile">View more</span>');
        pinJs.changeArrowTitle('lesson-collapse', 'active', '<span class="hide-on-mobile">View more lessons</span>');
        pinJs.changeArrowTitle('tips-desc', 'active', '<span class="hide-on-mobile">View more</span>');
        pinJs.changeArrowTitle('dynamic-block-course', 'active', '<span class="hide-on-mobile">View more</span>');
        pinJs.changeArrowTitle('homework-desc', 'active', '<span class="hide-on-mobile">View more</span>');
    },

    changeArrowTitle: function (arrowid, classname, title) {
        if (arrowid == 'dynamic-block-course') {
            if ($('#' + arrowid).hasClass(classname)) {
                $('#' + arrowid).html('<img class="glyphicon trigger-collapses" src="/Assets/NewUI/images/song-detail/minus-icon.png"/>');
            } else {
                $('#' + arrowid).html('<img class="glyphicon trigger-collapses" src="/Assets/NewUI/images/song-detail/plus-icon.png"/>');
            }
        } else {
            if ($('#' + arrowid).hasClass(classname)) {
                $('#' + arrowid).html('<span class="hide-on-mobile">Collapse </span><span class="glyphicon glyphicon-menu-up trigger-collapses"></span>');
            } else {
                $('#' + arrowid).html(title + ' <span class="glyphicon glyphicon-menu-down trigger-collapses"></span>');
            }
        }

    },
    close_accordion_section: function () {
        $('.trigger-collapses').removeClass('active');
        $('.has-collapse').slideUp(300).removeClass('open');
    },
    fixHeightLessonDesc: function () {
        var heightBlockPlay = $(".block-play-video").outerHeight() - 50;
        var heightCollapse = heightBlockPlay - (59 * 2 + 60);
        $(".has-collapse").height(heightCollapse);
    },

    //InitPageForMember
    initForMemberPage: function (strItemCountList, strItemCountListHidden, page, pageSize) {
        SbAdmin.initForOverlay();
        WebRtcDemo.UpdateUserList();
        $('.btn-login-this-student').off('click');
        $('.btn-login-this-student').on('click', function (e) {
            e.preventDefault();
            window.pinJs.logintoStudent($(this).attr('data-mail'));
        });
        $(strItemCountList).text($(strItemCountListHidden).val());
        $('.btn-edit-for-member').off('click');
        $('.btn-edit-for-member').on('click', function () {
            var memberId = $(this).attr('data-mid'),
                numberActive = Number($('body').find('#student-count-ac').find('span').text()),
                numberInActive = Number($('body').find('#student-count-inactive').find('span').text());

            memberController.editItem(memberId, function (response) {
                $('#btn-student-filter-by-name').click();

                if (response && response.Success) {
                    var statusId = $('#frmAddOrEditMember').data('member-status-id');

                    switch (statusId) {
                        case '0':// inactive
                            numberActive = numberActive - 1;
                            $('body').find('#student-count-ac').find('span').text(numberActive);

                            numberInActive = numberInActive + 1;
                            $('body').find('#student-count-inactive').find('span').text(numberInActive);
                            break;
                        case '1':// active
                            numberActive = numberActive + 1;
                            $('body').find('#student-count-ac').find('span').text(numberActive);

                            numberInActive = numberInActive - 1;
                            $('body').find('#student-count-inactive').find('span').text(numberInActive);
                            break;
                    }
                    $('#frmAddOrEditMember').data('member-status-id', undefined);
                }
            });
        });
        $('.btn-delete-this-student').off('click');
        $('.btn-delete-this-student').on('click', function (e) {
            e.preventDefault();
            var memberId = $(this).attr('data-mid');
            Members.DeleteMember(memberId, function (response) {
                $('#btn-student-filter-by-name').click();
            });
        });

        if ($(strItemCountListHidden).val() < page * pageSize) {
            $('#student-see-more').parent().addClass('hidden');
        } else {
            $('#student-see-more').parent().removeClass('hidden');
        }
    },

    //Order LessonList
    orderLessonList: function (idFrom, idTarget, courseId, isHistory) {
        $.ajax(
        {
            url: '/LearningCenter/ReSortLesson',
            type: "POST",
            data: 'idFrom=' + idFrom + '&idTarget=' + idTarget + '&courseId=' + courseId + '&isHistory=' + isHistory,
            success: function (result, textStatus, jqXHR) {
                if (result.Success == true) {

                } else {
                    console.log(result.Errors);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    },

    getUrlParameter: function (sParam) {
        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
            sURLVariables = sPageURL.split('&'),
            sParameterName,
            i;

        for (i = 0; i < sURLVariables.length; i++) {
            sParameterName = sURLVariables[i].split('=');

            if (sParameterName[0] === sParam) {
                return sParameterName[1] === undefined ? true : sParameterName[1];
            }
        }
    },

    convertUTCDateToLocalDate: function (timestamp) {
        var date = new Date(timestamp * 1000);
        var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

        var offset = date.getTimezoneOffset() / 60;
        var hours = date.getHours();

        newDate.setHours(hours - offset);

        return newDate;
    },

    getTimezoneOffsetWithDST: function () {
        var rightNow = new Date();
        var jan1 = new Date(rightNow.getFullYear(), 0, 1, 0, 0, 0, 0);
        var temp = jan1.toGMTString();
        var jan2 = new Date(temp.substring(0, temp.lastIndexOf(" ") - 1));
        var std_time_offset = (jan1 - jan2) / (1000 * 60 * 60);

        var june1 = new Date(rightNow.getFullYear(), 6, 1, 0, 0, 0, 0);
        temp = june1.toGMTString();
        var june2 = new Date(temp.substring(0, temp.lastIndexOf(" ") - 1));
        var daylight_time_offset = (june1 - june2) / (1000 * 60 * 60);
        var dst;
        if (std_time_offset == daylight_time_offset) {
            dst = 0; // daylight savings time is NOT observed
        } else {
            dst = 1; // daylight savings time is observed
        }
        var timeOffSet = (new Date().getTimezoneOffset() + (dst * 60));
        return timeOffSet;
    },

    checkBrowser: function () {
        var brwsr = "Chrome";
        var c = navigator.userAgent.search("Chrome");
        var f = navigator.userAgent.search("Firefox");
        var m8 = navigator.userAgent.search("MSIE 8.0");
        var m9 = navigator.userAgent.search("MSIE 9.0");
        if (c > -1) {
            brwsr = "Chrome";
        } else if (f > -1) {
            brwsr = "Firefox";
        } else if (m9 > -1) {
            brwsr = "MSIE 9.0";
        } else if (m8 > -1) {
            brwsr = "MSIE 8.0";
        }
        return brwsr;
    },

    getYoutubeIdFromUrl: function (youtubeUrl) {
        var videoid = youtubeUrl.match(/(?:https?:\/{2})?(?:w{3}\.)?youtu(?:be)?\.(?:com|be)(?:\/watch\?v=|\/)([^\s&]+)/);
        if (videoid != null) {
            return videoid[1];
        } else {
            return '';
        }
    },

    //Log Video
    logVideoPlay: function (isPlayed, playingToken, logType, courseFileId, fileToken, courseId, lessonId, homeworkId, callback) {
        if (isPlayed === 0 || fileToken !== playingToken) {
            var url = '/LearningCenter/LogVideo';
            var data = {
                LogType: logType,
                CourseFileID: courseFileId,
                FileToken: fileToken,
                CourseID: courseId,
                LessonID: lessonId,
                HomeworkID: homeworkId
            }
            $.post(url, data, function (response) {
                if (response.Status === true) {
                    if (callback && typeof (callback) == 'function') {
                        callback(response);
                    }
                }
            });
        }
    },
    logVideoSlowDown: function (courseFileId, fileToken, courseId, lessonId, slowPercent, homeworkId, callback) {
        var VideoSlowDown = 3; //LogType of VideoSlowdown
        if (homeworkId > 0) {
            VideoSlowDown = 11; //LogType of HomeworkVideoSlowDown
        }

        var url = '/LearningCenter/LogVideo';
        var data = {
            LogType: VideoSlowDown,
            CourseFileID: courseFileId,
            FileToken: fileToken,
            CourseID: courseId,
            LessonID: lessonId,
            SlowPercent: slowPercent,
            HomeworkID: homeworkId
        }
        $.post(url, data, function (response) {
            if (response.Status === true) {
                if (callback && typeof (callback) == 'function') {
                    callback(response);
                }
            }
        });
    },
    logVideoLoop: function (logType, courseFileId, fileToken, courseId, lessonId, homeworkId, callback) {
        var url = '/LearningCenter/LogVideo';
        var data = {
            LogType: logType,
            CourseFileID: courseFileId,
            FileToken: fileToken,
            CourseID: courseId,
            LessonID: lessonId,
            HomeworkID: homeworkId
        }
        $.post(url, data, function (response) {
            if (response.Status === true) {
                if (callback && typeof (callback) == 'function') {
                    callback(response);
                }
            }
        });
    },
    drawCurveTypes: function (charId, arrCol, itemChart, arrColor, titleChart) {
        //debugger;
        var data = new google.visualization.DataTable();;
        data.addColumn('string', 'point');
        $.each(arrCol, function (index, value) {
            data.addColumn('number', value);
        });
        data.addRows(itemChart);
        var valueShow = '';
        if ($(window).width() < 768) {
            valueShow = 'none';
        }
        var options = {
            backgroundColor: 'transparent',
            series: {
                //1: { curveType: 'line' }

            },
            vAxis: { maxValue: 10 },
            chartArea: { left: 40, width: '70%' },
            pointSize: 5,
            colors: arrColor,
            legend: valueShow
        };

        var chart = new google.visualization.LineChart(document.getElementById(charId));
        chart.draw(data, options);
    },
    drawPieChart: function (charId, arrData, arrPieColor, titleChart, isEmpty) {
        var data = google.visualization.arrayToDataTable(arrData);
        var valueShow = '';
        var vTrigger = '';
        var vPieSliceText = 'percentage';
        if ($(window).width() < 480) {
            valueShow = 'none';
        }
        if (isEmpty && isEmpty == 1) {
            valueShow = 'none';
            vTrigger = 'none';
            vPieSliceText = 'label';
        }

        var options = {
            backgroundColor: 'transparent',
            chartArea: { 'width': '70%', 'height': '70%' },
            pieSliceText: vPieSliceText,
            tooltip: { trigger: vTrigger },
            colors: arrPieColor,
            legend: valueShow,
            is3D: true
        };

        var chart = new google.visualization.PieChart(document.getElementById(charId));
        chart.draw(data, options);
    },
    drawTwoLine: function () {
        $('text').each(function (i, el) {
            if (years.indexOf(el.textContent) != -1) {
                var g = el.parentNode;
                var x = el.getAttribute('x');
                var y = el.getAttribute('y');
                var width = el.getAttribute('width') || 100;
                var height = el.getAttribute('height') || 35;

                // A "ForeignObject" tag is how you can inject HTML into an SVG document.
                var fo = document.createElementNS("http://www.w3.org/2000/svg", "foreignObject");
                fo.setAttribute('x', x - xDelta);
                fo.setAttribute('y', y - yDelta);
                fo.setAttribute('height', height);
                fo.setAttribute('width', width);

                var body = document.createElementNS("http://www.w3.org/1999/xhtml", "BODY");
                var p = document.createElement("P");

                p.setAttribute("style", "font-size:.8em; font-family:Arial;text-align:center;color:#000;");
                p.innerHTML = el.textContent.replace('|', '<br />');
                body.appendChild(p);
                fo.appendChild(body);
                // Remove the original SVG text and replace it with the HTML.
                g.removeChild(el);
                g.appendChild(fo);
            }
        });
    },

    genGuid: function () {
        return pinJs.getS4Guid() + pinJs.getS4Guid() + '-' + pinJs.getS4Guid() + '-' + pinJs.getS4Guid() + '-' +
            pinJs.getS4Guid() + '-' + pinJs.getS4Guid() + pinJs.getS4Guid() + pinJs.getS4Guid();
    },
    getS4Guid: function () {
        return Math.floor((1 + Math.random()) * 0x10000).toString(16).substring(1);
    },
    removeItemInArray: function (arr) {
        var what, a = arguments, L = a.length, ax;
        while (L > 1 && arr.length) {
            what = a[--L];
            while ((ax = arr.indexOf(what)) !== -1) {
                arr.splice(ax, 1);
            }
        }
        return arr;
    },

    isMobile: function () {
        var isMobile = false; //initiate as false
        // device detection
        if (/(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|ipad|iris|kindle|Android|Silk|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino/i.test(navigator.userAgent)
            || /1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-/i.test(navigator.userAgent.substr(0, 4))) isMobile = true;

        return isMobile;
    },
    loadingCardNumber: function (number, frm) {
        var cardName = "";
        var cards = new Array(
            {
                Name: "maestro",
                Pattern: "^(5(018\\d{8}|018\\d{9}|018\\d{10}|018\\d{11}|018\\d{12}|018\\d{13}|018\\d{14}|018\\d{15}|0[23]\\d{9}|0[23]\\d{10}|0[23]\\d{11}|0[23]\\d{12}|0[23]\\d{13}|0[23]\\d{14}|0[23]\\d{15}|0[23]\\d{16}|[68]\\d{10}|[68]\\d{11}|[68]\\d{12}|[68]\\d{13}|[68]\\d{14}|[68]\\d{15}|[68]\\d{16}|[68]\\d{17})|6(39\\d{9}|39\\d{10}|39\\d{11}|39\\d{12}|39\\d{13}|39\\d{14}|39\\d{15}|39\\d{16}|7\\d{10}|7\\d{11}|7\\d{12}|7\\d{13}|7\\d{14}|7\\d{15}|7\\d{16}|7\\d{17}))"
            },
            {
                Name: "forbrugsforeningen",
                Pattern: "^600\\d{13}$"
            },
            {
                Name: "dankort",
                Pattern: "^600\\d{13}$"
            },
            {
                Name: "visa",
                Pattern: "^4\\d{12}|4\\d{15}$"
            },
            {
                Name: "mastercard",
                Pattern: "^(5[0-5]|2[2-7])\\d{14}$"
            },
            {
                Name: "amex",
                Pattern: "^3[47]\\d{13}$"
            },
            {
                Name: "dinersclub",
                Pattern: "^3[0689]\\d{12}"
            },
            {
                Name: "discover",
                Pattern: "^6([045]\\d{14}|22\\d{13})$"
            },
            {
                Name: "unionpay",
                Pattern: "^(62|88)\\d{14}$"
            },
            {
                Name: "jcb",
                Pattern: "^35\\d{14}$"
            }
        );
        for (var i = 0; i < cards.length; i++) {
            var regex = new RegExp(cards[i].Pattern);
            if (regex.test(number)) {
                cardName = cards[i].Name;
            }
        }
        $(frm + " #credit-card-ico").removeClass();
        $(frm + " #credit-card-ico").addClass(cardName);
        //$(zone + " " + frm + " #credit-card-ico").addClass(cardName);
        $(frm + " #CardType").val(cardName);
    },

    ajaxLoadForDiv: function (strContainerId, urlAjax, data, typeAjax, callbackSuccess) {
        var container = $('#' + strContainerId);
        var url = urlAjax;
        var htmlAjaxLoading = '<div class="text-center" id="ajax-loading-list-' + strContainerId + '"><img src="/Assets/NewUI/images/ajax-loader.gif" alt="loading"></div>';
        container.html(htmlAjaxLoading);
        container.addClass('pending');

        $.ajax({
            type: typeAjax,
            url: url,
            data: data,
            success: function (result) {
                container.html(result);
                $('#ajax-loading-list-' + strContainerId).addClass('hidden');
                container.removeClass('pending');
                if (callbackSuccess && typeof (callbackSuccess) == 'function') {
                    callbackSuccess(result);
                }
            }
        });
    },

    blockUIForLoading: function () {
        $.blockUI({
            css: {
                border: 'none',
                'z-index': '10099',
                padding: '15px',
                backgroundColor: '#000',
                '-webkit-border-radius': '10px',
                '-moz-border-radius': '10px',
                opacity: .5,
                color: '#fff'
            }
        });
        $(".blockUI.blockOverlay").css("zIndex", 10099);
    },

    unBlockUIForLoading: function () {
        $.unblockUI();
    },

    getTimezoneOffsetNoDST: function () {
        var currentTimeZone = moment.tz.guess();
        var zone = moment.tz.zone(currentTimeZone);
        var timeOffset;

        if (currentTimeZone === 'Pacific/Easter') {
            timeOffset = 360;
        } else if (currentTimeZone === 'America/Santiago') {
            timeOffset = 240;
        } else if (currentTimeZone === 'America/Sao_Paulo') {
            timeOffset = 180;
        } else if (currentTimeZone === 'Australia/Lord_Howe') {
            timeOffset = -630;
        } else if (currentTimeZone === 'Pacific/Auckland' || currentTimeZone === 'Pacific/Fiji') {
            timeOffset = -720;
        } else if (currentTimeZone === 'Pacific/Chatham') {
            timeOffset = -765;
        } else if (currentTimeZone === 'Pacific/Apia') {
            timeOffset = -780;
        } else if (currentTimeZone === 'America/Asuncion' || currentTimeZone === 'America/Cuiaba' || currentTimeZone === 'Africa/Windhoek' || currentTimeZone === 'Australia/Adelaide' || currentTimeZone === 'Australia/Sydney' || currentTimeZone === 'Australia/Hobart') {
            timeOffset = Math.max.apply(Math, zone.offsets);
        } else {
            timeOffset = zone.offset(1388563200000);
        }

        return timeOffset;
    },

    donAllowInputTextToDp: function (id) {
        $(id).keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 191]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });
    },
    msgAlerModal: function (options, titleHeader) {
        var settings = {
            type: 'confirm',
            buttons: [
                {
                    //addClass: 'btn btn-primary',
                    addClass: 'btn btn-primary btn-crm btn-ok',
                    text: 'OK',
                    onClick: function ($noty) {
                        $noty.close();
                    },
                    focus: false

                }
            ],
            modal: true,
            titleHeader: titleHeader
        };
        $.extend(true, settings, options);
        this.alert(settings);
    },
    ApprovePlan: function (planId, zone, popup, reason) {
        window.pinJs.msgConfirm({
            text: "Are you sure that you want to Approve this Plan?",
            buttons: [
                {
                    text: 'Yes',
                    onClick: function ($noty) {
                        $noty.close();
                        $.ajax({
                            url: "/Plan/Approve/",
                            type: "POST",
                            dataType: "json",
                            data: {
                                id: planId,
                                status: "approve",
                                reason: reason
                            },
                            success: function (response) {
                                if (response.Success) {
                                    if (popup) {
                                        $("#PlanList").data("kendoGrid").dataSource.page(1);
                                    }

                                }
                                alertify.success(response.Message);
                            },
                            error: function (xhr, ajaxOptions, thrownError) {
                            }
                        });
                    }
                },
                {
                    text: 'No',
                    onClick: function ($noty) {
                        $noty.close();
                    }
                }
            ]
        });
    },

    getPosition: function (el) {
        var xPos = 0;
        var yPos = 0;

        while (el) {
            if (el.tagName == "BODY") {
                // deal with browser quirks with body/window/document and page scroll
                var xScroll = el.scrollLeft || document.documentElement.scrollLeft;
                var yScroll = el.scrollTop || document.documentElement.scrollTop;

                xPos += (el.offsetLeft - xScroll + el.clientLeft);
                yPos += (el.offsetTop - yScroll + el.clientTop);
            } else {
                // for all other non-BODY elements
                xPos += (el.offsetLeft - el.scrollLeft + el.clientLeft);
                yPos += (el.offsetTop - el.scrollTop + el.clientTop);
            }

            el = el.offsetParent;
        }
        return {
            x: xPos,
            y: yPos
        };
    },

    SnapShotCroped: function (element, formatOutput, myCallback) {
        window.pinJs.blockUIForLoading();
        window.scrollTo(0, 0);

        var keys = { 37: 1, 38: 1, 39: 1, 40: 1 };

        function preventDefault(e) {
            e = e || window.event;
            if (e.preventDefault)
                e.preventDefault();
            e.returnValue = false;
        }

        function preventDefaultForScrollKeys(e) {
            if (keys[e.keyCode]) {
                preventDefault(e);
                return false;
            }
        }

        function disableScroll() {
            if (window.addEventListener) // older FF
                window.addEventListener('DOMMouseScroll', preventDefault, false);
            window.onwheel = preventDefault; // modern standard
            window.onmousewheel = document.onmousewheel = preventDefault; // older browsers, IE
            window.ontouchmove = preventDefault; // mobile
            document.onkeydown = preventDefaultForScrollKeys;
        }

        function enableScroll() {
            if (window.removeEventListener)
                window.removeEventListener('DOMMouseScroll', preventDefault, false);
            window.onmousewheel = document.onmousewheel = null;
            window.onwheel = null;
            window.ontouchmove = null;
            document.onkeydown = null;
        }

        disableScroll();
        html2canvas(document.body, {
            "onrendered": function (canvas) { //get entire div "snapshot"
                var context = canvas.getContext('2d'); //context from originalCanvas

                var tmpCanvas = document.createElement('canvas');
                tmpCanvas.width = canvas.width;
                tmpCanvas.height = canvas.height;
                var context2 = canvas.getContext('2d'); //context from tmpCanvas

                var myElement = document.querySelector("#" + element);
                var position = pinJs.getPosition(myElement);

                var imageObj = new Image();

                imageObj.onload = function () {
                    //setup: draw cropped image
                    var sourceX = position.x + 8;
                    var sourceY = position.y;
                    var sourceWidth = $("#" + element).width() + 6;
                    var sourceHeight = $("#" + element).height() - 4;
                    var destWidth = sourceWidth;
                    var destHeight = sourceHeight;
                    var destX = canvas.width / 2 - destWidth / 2;
                    var destY = canvas.height / 2 - destHeight / 2;
                    context2.drawImage(imageObj, sourceX, sourceY, sourceWidth, sourceHeight, destX, destY, destWidth, destHeight);
                    var data = context2.getImageData(sourceX, sourceY, sourceWidth, sourceHeight);

                    context.clearRect(0, 0, canvas.width, canvas.height); //clear originalCanvas
                    canvas.width = sourceWidth;
                    canvas.height = sourceHeight;

                    context2.putImageData(data, 0, 0);

                    myCallback(canvas.toDataURL(formatOutput));

                    //memory!!!
                    context.clearRect(0, 0, sourceWidth, sourceHeight); //clear originalCanvas
                    context2.clearRect(0, 0, sourceWidth, sourceHeight); //clear tmpCanvas
                    data = null;
                    tmpCanvas = null;
                    canvas = null;
                    imageObj = null;
                };
                imageObj.src = tmpCanvas.toDataURL("image/png");
                enableScroll();
            }
        });
    },

    openPopupGeneratePayroll: function () {
        var options = {
            id: "popupGeneratePayroll",
            width: 1200,
            title: false,
            draggable: false,
            resizable: false
        };
        var popupWindow = pinJs.initPopupWindow(options);
        popupWindow.refresh({
            url: "/Payroll/GeneratePayroll",
            async: false
        }).center().open();
        $('#popupGeneratePayroll').addClass('scroll');
        $("#popupGeneratePayroll").parent().addClass("mobileModal");
    },

    GeneratePayrollOnChangeSelectTeacher: function (e) {
        var lst = [];
        var listSchool = $("#frmGeneratePayrollReport #ListSelectTeacher").data("kendoMultiSelect").dataItems();
        var listTeacher = $("#frmGeneratePayrollReport #ListSelectTeacher").data("kendoMultiSelect");
        for (var i = 0; i < listSchool.length; i++) {
            if (i === 0 && parseInt(listSchool[i].Value) === 0 && listSchool.length > 1) {
                continue;
            } else {
                if (parseInt(listSchool[i].Value) === 0) {
                    lst = [0];
                    break;
                }
                else {
                    lst.push(listSchool[i].Value);
                }
            }
        }
        listTeacher.setDataSource(listTeacher.dataSource.data());
        listTeacher.value(lst);
    },

    ListGridAdditionalPayrollPeriodSummary: function () {
        return {
            payrollPeriodId: $("#page-generate-payroll-summary #payrollPeriodId").val(),
            isGenerate: $("#page-generate-payroll-summary #isGenerate").val()
        };
    },

    ListGridAdditionalPayrollPeriodSummaryGenerate: function () {
        return {
            payrollPeriodId: $("#page-generate-payroll #page-generate-payroll-summary #payrollPeriodId").val(),
            isGenerate: $("#page-generate-payroll #page-generate-payroll-summary #isGenerate").val()
        };
    },

    ListGridAdditionalPayrollPeriodSummaryDetail: function () {
        return {
            payrollPeriodId: $("#page-generate-payroll-summary #payrollPeriodId").val(),
            periodSummaryId: $("#page-generate-payroll-summary #periodSummaryId").val(),
            timeOffSet: $("#page-generate-payroll-summary #timeOffSet").val()
        };
    },

    ListGridAdditionalPayrollPeriodSummaryDetailGenerate: function () {
        return {
            payrollPeriodId: $("#page-generate-payroll #page-generate-payroll-summary #payrollPeriodId").val(),
            periodSummaryId: $("#page-generate-payroll #page-generate-payroll-summary #periodSummaryId").val(),
            timeOffSet: $("#page-generate-payroll #page-generate-payroll-summary #timeOffSet").val()
        };
    },

    showPayrollSummaryDetail: function (payrollPeriodId, periodSummaryId, isGenerate) {
        var containerId = '.tz-payroll';
        var timeOffSet = (-1 * (window.pinJs.getTimezoneOffsetNoDST()));
        var data = {
            payrollPeriodId: payrollPeriodId,
            periodSummaryId: periodSummaryId,
            isGenerate: isGenerate,
            timeOffSet: timeOffSet
        }
        if (isGenerate == true) {
            containerId = '#page-generate-payroll';
        }

        $.get('/Payroll/GetPayrollPeriodSummaryDetail', data, function (response) {
            $(containerId + ' #page-generate-payroll-detail').html(response);
            $(containerId + ' #page-generate-payroll-detail').removeClass('hidden');
        });
    },

    openPopupEditReimbursementSummary: function (periodSummaryId) {
        var options = {
            id: "popupEditReimbursementSummary",
            width: 300,
            title: false,
            draggable: false,
            resizable: false
        };
        var popupWindow = pinJs.initPopupWindow(options);
        popupWindow.refresh({
            url: "/Payroll/PopupEditReimbursementSummary?periodSummaryId=" + periodSummaryId,
            async: false
        }).center().open();
        $('#popupGeneratePayroll').addClass('scroll');
        $("#popupGeneratePayroll").parent().addClass("mobileModal");
        window.pinJs.initFormWindow('#frmEditReimbursementSummary', popupWindow, $('#page-generate-payroll #PayrollPeriodSummaryListGenerate'));
    },

    eventGridPayrollPeriodSummaryDetailDataBound: function () {
        window.pinJs.replaceOneItemAllCourse();
        $('#PayrollPeriodSummaryDetailList .k-grouping-row .k-reset').contents().filter(function () {
            return this.nodeType == 3;
        }).each(function () {
            this.textContent = this.textContent.replace('Name: ', '');
        });
    },
    getURLParameter: function (name) {
        return decodeURIComponent((new RegExp('[?|&]' + name + '=' + '([^&;]+?)(&|#|;|$)').exec(location.search) || [, ""])[1].replace(/\+/g, '%20')) || null;
    },

    //StoreBuilder List
    getDataStoreInfoList: function () {
        return {
            idCurrentUser: $('#txt-mem-id').val()
        }
    }
});
//Init setaJs
window.pinJs.init();