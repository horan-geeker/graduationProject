
var queSeq = 0;

var utility = {};

//a的序列要比b靠前，这样a肯定有next，b至少有prev
//a和b交换位置就是把b插到a的后一个元素的前面，把a插到b的前一个元素的后面


//startIndex代表的是起始行
utility.number = function (startIndex) {
    var rowNum = $("#question-div").find("tr").length;
    if (rowNum == 0) {
        var row = $("#question-div").find("tr");
        var col = $(row).find("td").eq(0);
        var div = $(col).find("div")
        $(div).html(1);
    }
    for (var i = startIndex; i < rowNum; i++) {
        var row = $("#question-div").find("tr").eq(i);
        var col = $(row).find("td").eq(0);
        var div = $(col).find("div")
        $(div).html(i + 1);
    }
}




utility.getButton = function (id, value) {
    return '<input type="Button" id="' + id + ' " value="' + value + ' "/> ';
}

utility.getButtonByName = function (name, value) {
    return '<input type="Button" name="' + name + '" value=" ' + value + ' "/> ';
}

utility.getDropDownSelect = function (name) {
    var dropDownSelect = '<select name="' + name + '"></select> ';
    return dropDownSelect;
}

utility.getTextArea = function (title, name) {
    return '<span>' + title + '</span> <span> <textarea name="' + name + '" cols="60" rows="2"></textarea> </span>';
}

utility.getSpan = function (name) {
    return '<span name=' + name + '></span>';
}

utility.getQueDiv = function () {
    var newDiv = "<div class='questionPanel'></div>";
    return newDiv;
}

utility.getUl = function (cla) {
    var newUl = "<ul class='" + cla + "'></ul>";
    return newUl;
}

utility.getDiv = function (cla) {
    var newDiv = "<div class='" + cla + "'></div>";
    return newDiv;
}

utility.getRow = function (cla) {
    var newDiv = "<div class='" + cla + "'></div>";
    return newDiv;
}

utility.getQueTitle = function (number) {
    var newDiv = "<div class='col-md-12'><div class='input-group'>"
				+ "<div class='input-group-addon queSeq'>" + number + "</div>"
				+ "<textarea class='form-control' name='questionContent' placeholder='请输入题目' rows='1'>"
				+ "</textarea></div></div>";
    return newDiv;
}

utility.getQueTitleWithValue = function (number, value) {
    var newDiv = "<div class='col-md-12'><div class='input-group'>"
				+ "<div class='input-group-addon queSeq'>" + number + "</div>"
				+ "<textarea class='form-control' name='questionContent' placeholder='请输入题目' rows='1'>"
                + value
				+ "</textarea></div></div>";
    return newDiv;
}

utility.getChoice = function () {
    var radiodiv = $(utility.getDiv("choice-div"));

    var li = $(utility.getLi());




    var input = $("<input type='text' class='choice' value='' name='choice' placeholder='请输入选项内容' requeired/>");
    input.appendTo(radiodiv);
    // row3.html(input);

    //第二步：添加四个操作按钮,并且为每个按钮绑定事件
    /*	var id = $("[id^='upChoiceBtn']").size() + 1;
        var upChoiceBtn = $(utility.getButton("upChoiceBtn" + id, ""));
        upChoiceBtn.on("click",function(event) {// 为“添加”按钮添加事件
                    //utility.up(this);
                    alert();
                });
        
        id = $("[id^='downChoiceBtn']").size() + 1;
        var downChoiceBtn = $(utility.getButton("downChoiceBtn" + id, ""));
        downChoiceBtn.on("click",function(event) {// 为“添加”按钮添加事件
                    //utility.down(this);
                    alert();
                });
        */
    var id = $("[id^='addChoiceBtn']").size() + 1;
    var addChoiceBtn = $(utility.getButton("addChoiceBtn" + id, ""));
    addChoiceBtn.on("click", function (event) {// 为“添加”按钮添加事件
        //alert("添加选项");
        utility.addNewChoice(this);
    });

    id = $("[id^='delChoiceBtn']").size() + 1;
    var delChoiceBtn = $(utility.getButton("delChoiceBtn" + id, ""));
    delChoiceBtn.on("click", function (event) {// 为“添加”按钮添加事件
        utility.delChoice(this);
    });

    //为按钮添加class用于css控制样式
    //upChoiceBtn.addClass("btn-up");
    //downChoiceBtn.addClass("btn-down");
    addChoiceBtn.addClass("btn-add");
    delChoiceBtn.addClass("btn-del");

    //第2列放置功能按钮
    //upChoiceBtn.appendTo(radiodiv);
    //downChoiceBtn.appendTo(radiodiv);
    addChoiceBtn.appendTo(radiodiv);
    delChoiceBtn.appendTo(radiodiv);

    radiodiv.appendTo(li);


    return li;
}

utility.getQueCount = function () {
    var ques = $("[class^='questionPanel']");
    var count = ques.size();
    return count;
}

utility.sortSeq = function () {
    var ques = $("[class^='questionPanel']");
    for (var i = 0; i < ques.size() ; i++) {

        var seq = $(ques[i]).find(".queSeq");
        //alert(seq.html());
        seq.html(i + 1);
    };

}

//插入一个新的问题
utility.insertNewQuestion = function (obj) {

    var nowRow = $(obj).parent().parent().parent();
    var newQuestionPanel = $(utility.getNewQuestionPanel());
    newQuestionPanel.fadeIn();
    nowRow.after(newQuestionPanel);

    utility.sortSeq();
    utility.bindChangeType();
    //var hang = $(newQuestionPanel).prevAll().length;//代表的是新插入的行的行号
    //$(utility.number(hang));//编号
}

//插入一个新的问题
utility.addNewQuestion = function (obj) {
    //queSeq++;
    var parent = $(obj);
    var newQuestionPanel = $(utility.getNewQuestionPanel());
    newQuestionPanel.fadeIn();
    newQuestionPanel.appendTo(parent);

    utility.sortSeq();
    utility.bindChangeType();
}


//插入一个新的问题
utility.addNewChoice = function (obj) {
    var li = utility.getChoice();
    var nowli = $(obj).parent().parent();
    nowli.after(li);
}

utility.appendNewChoice = function (obj) {
    var li = utility.getChoice();
    var ul = $(obj).parent().parent().next().find("ul");
    //alert(ul.html());
    li.appendTo(ul);
}

utility.getLi = function () {
    var newLi = "<li></li>";
    return newLi;
}

utility.getQueOperator = function (number) {
    var newDiv = "<div class='col-md-3'>"
               + "<div class='input-group'>"
               + "<div class='input-group-addon' for='queType'>选择题型</div>"
               + "<select class='form-control' id='queType' name='queType'>"
               + "<option value='1'>单选题</option>"
               // + "<option value='2'>多选题</option>"
               + "<option value='2'>问答题</option>"
               + "<option value='3'>论述题</option>"
               + "</select>"
               + "</div>"
               + "</div>"
               + "<div class='col-md-2'>"
               + "<div class='checkbox'>"
               + "<label>"
               + "<input type='checkbox' id='is_required' name='is_required' checked='true'>必做题</label>"
               + "</div>"
               + "</div>";
    return newDiv;
}

//用户也有可能想要在指定行下面添加一个题目，这个题目可能是从大题库在挑选出来的，也有可能是用户新编辑的
//对于用户的这个需求，我决定采用这样的方式来达到：
utility.delQue = function (obj) {
    if (utility.getQueCount() < 2) {
        alert("无法删除");
    }
    else {
        var nowRow = $(obj).parent().parent().parent();
        nowRow.fadeOut(function () {
            nowRow.remove();
            utility.sortSeq();
        });//淡出效果
    }
}

utility.delChoice = function (obj) {

    var nowRow = $(obj).parent().parent();
    var parent = nowRow.parent();
    var choices = parent.find("[class=choice]");
    if (choices.size() > 2) {
        nowRow.fadeOut(function () {
            nowRow.remove();
        });//淡出效果
    }
    else {
        alert("选项不能少于两个!");
    }
    //nowRow.remove();
}


utility.exchange = function (a_obj, b_obj) {
    var temp = a_obj.find("ul").html();

    var nowContent = a_obj.find("[name='questionContent']");
    var tempContent = nowContent.val();
    var upContent = b_obj.find("[name='questionContent']");
    nowContent.val(upContent.val());
    upContent.val(tempContent);


    var nowType = a_obj.find("[name='queType']");
    var tempType = nowType.val();
    var upType = b_obj.find("[name='queType']");
    nowType.val(upType.val());
    upType.val(tempType);



    // b_obj.html($(a_obj).html());
    var b_objChoices = b_obj.find(".choice");
    var a_objChoices = a_obj.find(".choice");



    a_obj.find("ul").html(b_obj.find("ul").html());
    b_obj.find("ul").html(temp);

    var b_objAdds = b_obj.find("[id^='addChoiceBtn']");
    var b_objDels = b_obj.find("[id^='delChoiceBtn']");
    var b_objNewChoices = b_obj.find(".choice");
    for (var i = 0; i < b_objAdds.size() ; i++) {
        $(b_objNewChoices[i]).val($(a_objChoices[i]).val());
        $(b_objAdds[i]).on("click", function () {
            utility.addNewChoice(this);
        });
        $(b_objDels[i]).on("click", function () {
            utility.delChoice(this);
        });
    };

    var a_objAdds = a_obj.find("[id^='addChoiceBtn']");
    var a_objDels = a_obj.find("[id^='delChoiceBtn']");
    var a_objNewChoices = a_obj.find(".choice");
    for (var i = 0; i < a_objAdds.size() ; i++) {
        $(a_objNewChoices[i]).val($(b_objChoices[i]).val());
        $(a_objAdds[i]).on("click", function () {
            utility.addNewChoice(this);
        });
        $(a_objDels[i]).on("click", function () {
            utility.delChoice(this);
        });
    };

    utility.changeType(nowType, a_obj)
    utility.changeType(upType, b_obj)
}


utility.upQue = function (obj) {
    //alert(obj);
    //var nowRow = $(obj).parent().parent().parent();//只交换的第一列的内容
    var nowRow = obj.parentElement.parentElement.parentElement;
    var ques = $("[class^='questionPanel']");
    var index = 0;
    for (var i = 0; i < ques.length; i++) {
        if (ques[i] == nowRow) {
            index = i;
        }
    }
    nowRow = $(nowRow);
    //alert(index);
    if (index > 0) {
        //var upRow = document.getElementById('questionnaireBody').rows[i - 1];

        var upRow = nowRow.prev();

        utility.exchange(nowRow, upRow);


        //$(nowRow).html(temp);
    } else {
        alert("无法上移");
    }
}

utility.downQue = function (obj) {
    var nowRow = obj.parentElement.parentElement.parentElement;
    var ques = $("[class^='questionPanel']");
    var index = 0;
    for (var i = 0; i < ques.length; i++) {
        if (ques[i] == nowRow) {
            index = i;
        }
    }
    nowRow = $(nowRow);
    if (index < ques.length - 1) {
        //var upRow = document.getElementById('questionnaireBody').rows[i - 1];

        var downRow = nowRow.next();

        utility.exchange(downRow, nowRow);


        //$(nowRow).html(temp);
    } else {
        alert("无法下移");
    }
}

//这个函数用于在编辑卷体的时候创建一个新的问题
utility.getNewQuestionPanel = function () {
    var div = $(utility.getQueDiv());
    var seq = queSeq;

    /*row1*/
    var row1 = $(utility.getDiv("row"));
    var queTitle = $(utility.getQueTitle(seq));
    queTitle.appendTo(row1);

    var errMsg = $(utility.getDiv("col-md-12 text-danger"));
    errMsg.attr("name", "que_err_msg");
    errMsg.appendTo(row1);
    /*row2*/

    var row2 = $(utility.getDiv("row"));
    row2.css("padding-top", 10);
    var queOperator = $(utility.getQueOperator("1"));
    queOperator.appendTo(row2);

    var opdiv = $(utility.getDiv("col-md-6 op-div"));

    var id = $("[id^='addQueChoiceBtn']").size() + 1;
    var addQueChoiceBtn = $(utility.getButtonByName("addQueChoiceBtn", "添加选项"));
    addQueChoiceBtn.addClass("btn btn-default").click(function (event) {// 为“添加”按钮添加事件
        //alert("添加选项");
        utility.appendNewChoice(this);
    });
    /*
	id = $("[id^='bashAddQueChoiceBtn']").size() + 1;
	var bashAddQueChoiceBtn = $(utility.getButton("bashAddQueChoiceBtn" + id, "批量添加"));
	bashAddQueChoiceBtn.addClass("btn btn-default").click(function(event) {// 为“添加”按钮添加事件
				//alert("批量添加");
			});
    */
    id = $("[id^='upQueBtn']").size() + 1;
    var upQueBtn = $(utility.getButton("upQueBtn" + id, ""));
    upQueBtn.addClass("btn-up").click(function (event) {// 为“添加”按钮添加事件
        utility.upQue(this);
        //alert("上移本题");
    });

    id = $("[id^='downQueBtn']").size() + 1;
    var downQueBtn = $(utility.getButton("downQueBtn" + id, ""));
    downQueBtn.addClass("btn-down").click(function (event) {// 为“添加”按钮添加事件
        utility.downQue(this);
        //alert("下移本题");
    });

    //id = $("[id^='addQueBtn']").size() + 1;
    //var addQueBtn = $(utility.getButton("addQueBtn" + id, ""));
    //addQueBtn.addClass("btn-add").click(function (event) {// 为“添加”按钮添加事件
    //    utility.insertNewQuestion(this);
    //});

    id = $("[id^='addQueBtn']").size() + 1;
    var addQueBtn = $(utility.getButton("addQueBtn" + id, ""));
    addQueBtn.val("添加新题");
    addQueBtn.addClass("btn btn-primary").click(function (event) {// 为“添加”按钮添加事件
        utility.insertNewQuestion(this);
    });


    id = $("[id^='delQueBtn']").size() + 1;
    var delQueBtn = $(utility.getButton("delQueBtn" + id, ""));
    delQueBtn.val("删除本题");
    delQueBtn.addClass("btn btn-danger").click(function (event) {// 为“添加”按钮添加事件F:\实验室\【项目】\教师发展中心-问卷调查\项目文件\Questionnaire3\Questionnaire\Controllers/DataBaseController.cs
        //alert("删除本题");
        utility.delQue(this);
    });

    //id = $("[id^='delQueBtn']").size() + 1;
    //var delQueBtn = $(utility.getButton("delQueBtn" + id, ""));
    //delQueBtn.addClass("btn-del").click(function (event) {// 为“添加”按钮添加事件
    //    //alert("删除本题");
    //    utility.delQue(this);
    //});

    addQueChoiceBtn.appendTo(opdiv);
    //bashAddQueChoiceBtn.appendTo(opdiv);
    upQueBtn.appendTo(opdiv);
    downQueBtn.appendTo(opdiv);
    addQueBtn.appendTo(opdiv);
    delQueBtn.appendTo(opdiv);

    opdiv.appendTo(row2);


    /*row3*/
    var row3 = $(utility.getDiv("row"));
    var ul = $(utility.getUl("choice-ul"));
    var coldiv = $(utility.getDiv("col-md-12"));

    for (var i = 0; i < 2; i++) {
        var li = utility.getChoice();
        li.appendTo(ul);
    };

    ul.appendTo(coldiv);
    row1.appendTo(div);
    row2.appendTo(div);
    coldiv.appendTo(row3);
    row3.appendTo(div)
    //第四步：为面板添加样式
    return div;
}

utility.getQuestionPanel = function (question) {
    var div = $(utility.getQueDiv());
    var seq = queSeq;

    /*row1*/
    var row1 = $(utility.getDiv("row"));
    var queTitle = $(utility.getQueTitleWithValue(seq, question.value));
    queTitle.appendTo(row1);

    /*row2*/

    var row2 = $(utility.getDiv("row"));
    row2.css("padding-top", 10);
    var queOperator = $(utility.getQueOperator("1"));
    queOperator.appendTo(row2);

    var opdiv = $(utility.getDiv("col-md-6 op-div"));

    var id = $("[id^='addQueChoiceBtn']").size() + 1;
    var addQueChoiceBtn = $(utility.getButtonByName("addQueChoiceBtn", "添加选项"));
    addQueChoiceBtn.addClass("btn btn-default").click(function (event) {// 为“添加”按钮添加事件
        //alert("添加选项");
        utility.appendNewChoice(this);
    });
    /*
	id = $("[id^='bashAddQueChoiceBtn']").size() + 1;
	var bashAddQueChoiceBtn = $(utility.getButton("bashAddQueChoiceBtn" + id, "批量添加"));
	bashAddQueChoiceBtn.addClass("btn btn-default").click(function(event) {// 为“添加”按钮添加事件
				//alert("批量添加");
			});
    */
    id = $("[id^='upQueBtn']").size() + 1;
    var upQueBtn = $(utility.getButton("upQueBtn" + id, ""));
    upQueBtn.addClass("btn-up").click(function (event) {// 为“添加”按钮添加事件
        utility.upQue(this);
        //alert("上移本题");
    });

    id = $("[id^='downQueBtn']").size() + 1;
    var downQueBtn = $(utility.getButton("downQueBtn" + id, ""));
    downQueBtn.addClass("btn-down").click(function (event) {// 为“添加”按钮添加事件
        utility.downQue(this);
        //alert("下移本题");
    });

    id = $("[id^='addQueBtn']").size() + 1;
    var addQueBtn = $(utility.getButton("addQueBtn" + id, ""));
    addQueBtn.val("添加新题");
    addQueBtn.addClass("btn btn-primary").click(function (event) {// 为“添加”按钮添加事件
        utility.insertNewQuestion(this);
    });

    id = $("[id^='delQueBtn']").size() + 1;
    var delQueBtn = $(utility.getButton("delQueBtn" + id, ""));
    delQueBtn.addClass("btn-del").click(function (event) {// 为“添加”按钮添加事件
        //alert("删除本题");
        utility.delQue(this);
    });

    addQueChoiceBtn.appendTo(opdiv);
    //bashAddQueChoiceBtn.appendTo(opdiv);
    upQueBtn.appendTo(opdiv);
    downQueBtn.appendTo(opdiv);
    addQueBtn.appendTo(opdiv);
    delQueBtn.appendTo(opdiv);

    opdiv.appendTo(row2);


    /*row3*/
    var row3 = $(utility.getDiv("row"));
    var ul = $(utility.getUl("choice-ul"));
    var coldiv = $(utility.getDiv("col-md-12"));

    for (var i = 0; i < 2; i++) {
        var li = utility.getChoice();
        li.appendTo(ul);
    };

    ul.appendTo(coldiv);
    row1.appendTo(div);
    row2.appendTo(div);
    coldiv.appendTo(row3);
    row3.appendTo(div)
    //第四步：为面板添加样式
    return div;
}



function deleteQueSelect(obj) {
    var nowDiv = $(obj).parent();
    $(nowDiv).fadeOut('slow');//淡出效果
    $(nowDiv).remove();
    $(utility.tag());

}

utility.tag = function (queSelects) {
    var selectTags = $(queSelects).find("span[name='selectTag']");
    $.each(selectTags, function (i, n) {
        $(this).html(String.fromCharCode(65 + i));
    })

}

//对问题进行编号，在不同的操作中，startIndex和endIndex不同
//在delet中，startIndex
//在add中
utility.number = function (obj, startIndex, endIndex) {

}


utility.bindChangeType = function () {
    var select = $("[name='queType']");
    for (var i = 0; i < select.length; i++) {
        var nowSelect = $(select[i]);
        //var choiceRow = parent.next();
        nowSelect.on("change", function () {
            utility.changeType(nowSelect);
        });
    }
}

utility.changeType = function (nowSelect) {
    var parent = nowSelect.parent().parent().parent().parent();
    //alert("o");
    var choiceRow = $(parent).find(".choice-ul");
    var addChoiceBtn = $(parent).find("[name='addQueChoiceBtn']");
    //alert(choiceRow.html());
    if (nowSelect.val() >= 3) {
        //alert(choiceRow);
        choiceRow.fadeOut();
        addChoiceBtn.attr('disabled', "disabled");
    }
    else {
        //alert(choiceRow);
        choiceRow.fadeIn();
        addChoiceBtn.removeAttr("disabled");
    }
}


function checkInput() {
    var sign = true;

    var end = $("#end_datetime");
    if (end.val() == null || end.val() == "") {
        $("#end_err_msg").html("请输入截至日期").css("display", "block");
        end.focus();
        return false;
    } else {
        $("#title_err_msg").css("display", "none");
    }

    var title = $("#title");
    if (title.val() == null || title.val() == "") {
        $("#title_err_msg").html("请输入问卷标题").css("display", "block");
        title.focus();
        return false;
    } else {
        $("#title_err_msg").css("display", "none");
    }

    var direction = $("#direction");
    if (direction.val() == null || direction.val() == "") {
        direction.focus();
        $("#direction_err_msg").html("请输入问卷说明").css("display", "block");
        return false;
    } else {
        $("#direction_err_msg").css("display", "none");
    }

    var types = $("[name='queType']");

    for (var i = 0; i < types.length; i++) {
        var seq = i + 1;

        var nowType = $(types[i]);
        var parent = nowType.parents(".questionPanel");
        var queContent = parent.find("[name='questionContent']");
        var queErrMsg = parent.find("[name='que_err_msg']");
        if (queContent.val().length > 0) {
            queErrMsg.css("display", "none");
            if (nowType.val() < 3) {
                var choiceArr = parent.find(".choice");
                var quesign = true;
                for (var j = 0 ; j < choiceArr.length; j++) {
                    if ($(choiceArr[j]).val().length < 1) {
                        $(choiceArr[j]).focus();
                        quesign = false;
                        break;
                    }
                }
                if (quesign) {
                    queErrMsg.css("display", "none");
                }
                else {
                    queErrMsg.css("display", "block").html("请输入选项内容");
                    return false;
                }
            }

        } else {
            queContent.focus();
            queErrMsg.css("display", "block").html("请输入问题题目");
            return false;
        }
    }
    $("#error_msg").css("display", "none");
    return true;
}

function finish(first,publish,preview) {
    rowNum = utility.getQueCount();

    if (publish) {
        if (!window.confirm("发布后不可修改，确认要发布吗?")) {
            return;
        }
    }

    if (rowNum == 0) {
        alert("请至少添加一道问题");
        //if(!confirm("还没有添加试题，是否继续添加？")){
        //	document.location="manageQuenaireAction";
        //}
    }
    else if (checkInput()) {
        //alert(rowNum);
        //$("#option").attr("value","finish");
        //alert($("#option").val());

        var contents = $("[name='questionContent']");
        var types = $("[name='queType']");
        var is_required = $("[name='is_required']")
        var questions = new Array();

        for (var i = 0; i < contents.length; i++) {
            var que = new Object();
            que.content = $(contents[i]).val();
            var typeId = $(types[i]).val();
            que.type = typeId;

            if($(is_required[i]).is(':checked'))
            {
                que.is_required = 1;
            }
            else{
                que.is_required = 0;
            }


            var parent = $(contents[i]).parent().parent().parent().next().next();

            var choiceArr = new Array();
            var choices = new Array();

            if (typeId < 3) {
                //alert("选择题");
                choiceArr = parent.find("[name='choice']");
                for (var j = 0; j < choiceArr.length; j++) {
                    var choice = new Object();
                    choice.choiceContent = $(choiceArr[j]).val();
                    choices[j] = choice;
                }
            }
            //alert(choices[0]);
            //alert(choices[1]);

            que.choices = choices;
            questions[i] = que;
        }

       // alert("ok");
      //  alert($("#direction").val())
        //alert(UM.getEditor('direction').getPlainTxt());

        if (publish) {
            var url = "/Questionnaire/Store";
            if ($("#questionnaire_id").val() != null && $("#questionnaire_id").val() != "") {
                url = "/Questionnaires/Edit";
            }
            $.ajax({
                type: "POST",
                url: url,
                cache: false,
                data: {
                    id: $("#questionnaire_id").val(),
                    venue_id: $("#venue_id").val(),
                    end_datetime: $("#end_datetime").val(),
                    title: $("#title").val(),
                    direction: $("#direction").val(),
                    type: $("#quenaire_type").val(),
                    publish:1,
                    //direction: UM.getEditor('direction').getPlainTxt(),
                    thanks_msg: $("#thanks_msg").val(),
                    questions: JSON.stringify(questions)
                },
                dataType: "json",
                success: function (data) {
                    var jsonData = eval(data);
                    if (jsonData.Error == null) {
                        var id = jsonData.id;
                        $("#questionnaire_id").val(id);
                        //alert("11");
                        //alert("id" + id);
                        window.location.href = "/Questionnaires/Success/" + id;
                    }
                    else {
                        alert("服务器端错误:" + jsonData.Error);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        } else {
            /*保存*/
            /**
             * ajax 发送
             */
            var url = "/Questionnaire/Store";
            if ($("#questionnaire_id").val() != null && $("#questionnaire_id").val() != "") {
                url = "/Questionnaire/Edit";
            }
            //alert($("#questionnaire_id").val());
            //alert(url);
         
         $.ajax({
                type: "POST",
                url: url,
                cache: false,
                data: {
                    meet_id: $("#meet_id").val(),
                    title: $("#title").val(),
                    direction: $("#direction").val(),
                    end_at: $("#end_datetime").val(),
                    type: $("#quenaire_type").val(),
                    //publish: 0,
                    //direction: UM.getEditor('direction').getPlainTxt(),
                    //thanks_msg: $("#thanks_msg").val(),
                    questions: JSON.stringify(questions)
                },
                dataType: "json",
                success: function (data) {
                    var jsonData = eval(data);
                    if(jsonData.Error == null){
                    //alert("success");
                    var id = jsonData.id;
                    //alert("11");
                    //alert("id" + id);
                    //window.location.href = "/Questionnaires/Success/" + id;
                    $("#questionnaire_id").val(id);
                    //var message = $("#error_msg");
                    //message.css("display", "block").html("保存成功!");
                    console.log(data);
                    if (data.status == 0) {
                        window.location.href = "/Questionnaire/Show/" + data.data.id;
                    }
                    } else {
                        alert("服务器端错误:"+jsonData.Error);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
    }
}

//function editFinish() {
//    rowNum = utility.getQueCount();

//    if (rowNum == 0) {
//        alert("请至少添加一道问题");
//        //if(!confirm("还没有添加试题，是否继续添加？")){
//        //	document.location="manageQuenaireAction";
//        //}
//    }
//    else if (checkInput()) {
//        alert(rowNum);
//        //$("#option").attr("value","finish");
//        //alert($("#option").val());

//        var contents = $("[name='questionContent']");
//        var types = $("[name='queType']");
//        var questions = new Array();

//        for (var i = 0; i < contents.length; i++) {
//            var que = new Object();
//            que.content = $(contents[i]).val();
//            var typeId = $(types[i]).val();
//            que.type = typeId;

//            var parent = $(contents[i]).parent().parent().parent().next().next();

//            var choiceArr = new Array();
//            var choices = new Array();

//            if (typeId < 3) {
//                //alert("选择题");
//                choiceArr = parent.find("[name='choice']");
//                for (var j = 0; j < choiceArr.length; j++) {
//                    var choice = new Object();
//                    choice.choiceContent = $(choiceArr[j]).val();
//                    choices[j] = choice;
//                }
//            }
//            //alert(choices[0]);
//            //alert(choices[1]);

//            que.choices = choices;
//            questions[i] = que;
//        }

//        //alert("ok");
//       // alert($("#direction").val())
//        //alert(UM.getEditor('direction').getPlainTxt());

//        $.ajax({
//            type: "POST",
//            url: "/Questionnaires/Edit",
//            cache: false,
//            data: {
//                id: $("#questionnaire_id").val(),
//                venue_id: $("#venue_id").val(),
//                end_datetime: $("#end_datetime").val(),
//                title: $("#title").val(),
//                direction: $("#direction").val(),
//                //direction: UM.getEditor('direction').getPlainTxt(),
//                thanks_msg: $("#thanks_msg").val(),
//                questions: JSON.stringify(questions)
//            },
//            dataType: "json",
//            success: function (data) {
//                var jsonData = eval(data);
//               // alert("success");
//                var id = jsonData.id;
//                alert("11");
//                alert("id" + id);
//                window.location.href = "/Questionnaires/Success/" + id;
//            },
//            error: function (XMLHttpRequest, textStatus, errorThrown) {
//                alert(errorThrown);
//            }
//        });
//    }
//}
