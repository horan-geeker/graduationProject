(function () {

    app = {

        // init app
        init: function () {

            // config app
            this.config();
            this.table();
            this.form();
            this.sidebar();
        },

        // app config
        config: function() {
            $(function () {
                $('.datetimepicker').each(function () {
                    $(this).datetimepicker({ format: "YYYY-MM-DD HH:mm:00" });
                })

                var chart = document.getElementById('meet_chart');
                if (chart) {
                    var siginCount = chart.getAttribute('data-sigin');
                    var unsiginCount = chart.getAttribute('data-unsigin');
                    var myChart = echarts.init(chart);
                    // 指定图表的配置项和数据
                    option = {
                        title: {
                            text: '本次会议签到整体情况',
                            //subtext: '纯属虚构',
                            x: 'center'
                        },
                        tooltip: {
                            trigger: 'item',
                            formatter: "{a} <br/>{b} : {c} ({d}%)"
                        },
                        legend: {
                            orient: 'vertical',
                            left: 'left',
                            data: ['已签到', '未签到']
                        },
                        series: [
                            {
                                name: '签到情况',
                                type: 'pie',
                                radius: '55%',
                                center: ['50%', '60%'],
                                data: [
                                    { value: siginCount, name: '已签到' },
                                    { value: unsiginCount, name: '未签到' },
                                ],
                                itemStyle: {
                                    emphasis: {
                                        shadowBlur: 10,
                                        shadowOffsetX: 0,
                                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                                    }
                                }
                            }
                        ]
                    };

                    // 使用刚指定的配置项和数据显示图表。
                    myChart.setOption(option);
                    
                } else {
                    var charts = document.getElementsByClassName('answer_chart');
                    if (charts) {
                        for (var i = 0; i < charts.length; i++) {
                            var chart = charts[i];
                            (function (chart) {
                                $.ajax({
                                    type: 'post',
                                    url: '/Answer/ChartData',
                                    data: 'id=' + chart.getAttribute('data-id'),
                                    success: function (data) {
                                        l = [];
                                        counts = [];
                                        for (var i = 0; i < data.choice.length; i++) {
                                            l.push(data.choice[i].choice_content);
                                            counts.push(data.choice[i].count);
                                        }
                                        console.log(counts);
                                        // 指定图表的配置项和数据
                                        var option = {
                                            title: {
                                                text: data.title
                                            },
                                            tooltip: {},
                                            legend: {
                                                data: ['被选择数']
                                            },
                                            xAxis: {
                                                data: l
                                            },
                                            yAxis: {},
                                            series: [{
                                                name: '被选择数',
                                                type: 'bar',
                                                data: counts
                                            }]
                                        };
                                        var myChart = echarts.init(chart);
                                        myChart.setOption(option);
                                    }
                                });
                            })(chart)
                            
                        }
                    }
                }
            });
        },

        // app components
        form: function () {
            var options = {
                ignore: ".ignore",
                highlight: function (el) {
                    $(el).closest('.form-group').addClass('has-error');
                    // remove the error message from the server
                    $(el).closest('.form-group').find('.help-block').addClass('hidden');
                },
                unhighlight: function (el) {
                    $(el).closest('.form-group').removeClass('has-error');
                    // remove the error message from the server
                    $(el).closest('.form-group').find('.help-block').addClass('hidden');
                },
                errorPlacement: function (error, element) {
                    if (element.parent('.input-group').length) {
                        error.insertAfter(element.parent());
                    } else {
                        error.appendTo(element.closest('.form-group'));
                    }
                },
                /**
                submitHandler: function (form) {

                    // disable the submit buttons
                    $(form).find('.btn-submit, .btn-save').prop('disabled', true);

                    // decide which button to display the state on
                    var $clicked = $('.form-submitter.clicked');
                    if ($clicked.length) {
                        $clicked.html('<i class="fa fa-spinner fa-spin"></i>正在提交...');
                        return true;
                    }

                    // old code, may be removed later
                    $(form).find('.btn-submit').prop('disabled', true).html('<i class="fa fa-spinner fa-spin"></i>正在提交...');

                    return true;
                }
                **/
            };

            $('form:not(".should-manually-validate")').each(function () {
                $(this).validate(options);
                /**
                $(this).find('.btn-submit, .btn-save').addClass('form-submitter').on('click', function () {
                    $('.form-submitter').removeClass('clicked');
                    $(this).addClass('clicked');
                });
                **/
            });
        },

        // app table
        table: function () {
            // var $container = $('#page_content');
            // var $container = $(document).find('table tr td');

            // delegate deletion event
            $(document).find('table tr td').on('click', '.btn-delete', function (e) {
                // $container.on('click', '.btn-delete', function (e) {
                e.stopPropagation();
                e.preventDefault();

                // the button
                var $btn = $(this);

                // the row; the record
                var $tr = $(this).closest('tr');

                // prefer the url on the button
                var url = $btn.data('url') ? url = $btn.data('url') : $tr.data('url');

                var onConfirm = function () {

                    // disable button
                    $btn.prop('disabled', true);

                    $.ajax({
                        url: url,
                        type: 'post'
                    })
                        .done(function (resp) {
                            console.log("delete success");

                            // remove row
                            //$tr.remove();

                            // instead of removing row, refresh the page
                            location.reload();

                            // close modal
                            swal.close();
                        })
                        .fail(function (errors) {
                            console.log("delete error");

                            // it should never emit server error
                            try {
                                if (!_.isArray(errors)) {
                                    errors = JSON.parse(errors.responseText);
                                }

                                swal({
                                    title: "删除失败",
                                    text: errors.general ? errors.general : '服务器出错, 请稍后再试',
                                    type: "error"
                                });
                            } catch (e) {
                                swal({
                                    title: "删除失败",
                                    text: '服务器出错, 请稍后再试',
                                    type: "error"
                                });
                            }
                        })
                        .always(function () {
                            console.log("delete complete");

                            // enable button
                            $btn.prop('disabled', false);
                        });
                };

                // default warning message content
                var warningText = $btn.data('warning') ? $btn.data('warning') : '此操作不可逆';

                // init
                swal({
                    title: "确认删除?",
                    text: warningText,
                    type: "warning",
                    showCancelButton: true,
                    showLoaderOnConfirm: true,
                    closeOnConfirm: false
                }, onConfirm);
            });
        },

        // app global sidebar
        sidebar: function () {
            var sideBar = $("#site_nav_left_wrap");
            sideBar.find("a").each(function () {
                console.log($(this).attr("href"), window.location.pathname);
                if ($(this).attr("href") == window.location.pathname || $(this).attr("href") == window.location.pathname+"/Index" ) {
                    sideBar.find("a").removeClass("active");
                    $(this).addClass("active");
                }
            })
        }
    };
})();

app.init();