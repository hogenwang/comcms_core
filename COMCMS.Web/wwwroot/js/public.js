function init() {
    if (!IsPC()) {
        window_w = $(window).width();
        console.log(window.innerWidth);
        if (window_w >= 768) {
            $('#main').addClass('pc').removeClass('mb');
            resize(0, 1200, window_w);
        } else {
            $('#main').addClass('mb').removeClass('pc');
            resize(0, 640, window_w);
        }
        console.log('false: ' + IsPC());
        console.log('false: ' + window_w);
        JPlaceHolder.init();
    } else {
        window_w = window.screen.width;
        console.log('true: ' + window_w);
        $('#main').addClass('mb').removeClass('pc');
        resize(0, 640, window_w);
    }
}
function IsPC() {
    isPC = true;
    if ((navigator.userAgent.match(/(iPhone|iPod|Android|ios|iPad|Touch|PlayBook|SymbianOS|Windows Phone|Nokia)/))) {
        isPC = true;

    } else {
        isPC = false;
    }

    return isPC;
}
// 鑷€傚簲灞忓箷澶у皬
function resize(min_w, max_w, window_w) {
    // var font_size = Math.round((window_w/max_w*6.25)*10000)/100;
    font_size = Math.round((window_w / max_w * 6.25) * 10000) / 100;

    if (window_w >= min_w && window_w <= max_w) {
        $('html').css({ 'font-size': font_size + "%" });
    } else if (window_w > max_w) {
        $('html').css({ 'font-size': "625%" });
    }
}
var JPlaceHolder = {

    _check: function () {
        return 'placeholder' in document.createElement('input');
    },

    init: function () {
        if (!this._check()) {
            this.fix();
        }
    },

    fix: function () {
        jQuery(':input[placeholder]').each(function (index, element) {
            var self = $(this), txt = self.attr('placeholder');
            self.wrap($('<div></div>').css({ position: 'relative', zoom: '1', border: 'none', background: 'none', padding: 'none', margin: 'none' }));
            var pos = self.position(), h = self.outerHeight(true), paddingleft = self.css('padding-left');
            var holder = $('<span></span>').text(txt).css({ position: 'absolute', left: pos.left, top: pos.top, height: h, lienHeight: h, paddingLeft: paddingleft, color: '#aaa' }).appendTo(self.parent());
            self.focusin(function (e) {
                holder.hide();
            }).focusout(function (e) {
                if (!self.val()) {
                    holder.show();
                }
            });
            holder.click(function (e) {
                holder.hide();
                self.focus();
            });
        });
    }
};
function cds() {
    var deg = 0;
    var int;
    var hover = 0;
    $('.cds-list li').on('mouseover', function () {
        console.log(1);
        if (hover === 0) {
            var that = $(this);
            int = setInterval(function () {
                deg += 10;
                that.find('b')[0].style.transform = 'rotate(' + deg + 'deg)';
            }, 50);
            hover = 1;
        }
    }).on('mouseleave', function () {
        if (hover == 1) {
            clearInterval(int);
            deg = 0;
            $(this).find('b')[0].style.transform = 'rotate(0deg)';
            hover = 0;
        }
    });
}


// 导航栏
function navHover () {
	var top = 0;
	$(window).scroll(function () {
		top = $(this).scrollTop()
		if ( top !== 0 ) {
			$('.header').css({'background': '#fff'})
		} else {
			$('.header').css({'background': 'none'})
		}
	});

	$('.nav-list > li').on('mouseover', function () {
		if(top == 0) {
			$('.header').css({'background': '#fff'})
		}
		$('.header').css({'border-bottom': '1px solid #e9eaea'});
		var findRes = $(this).find('.sec-nav').length;
		var num = $('.sec-nav').length;
		if (findRes > 0) {
			for (var i = 0; i < num; i++) {
				$('.sec-nav').eq(i).slideUp();
			}
			if ($('.sec-nav').is(':visible')) {
				$(this).find('.sec-nav').stop().show();
			} else  {
				$(this).find('.sec-nav').slideDown();
			}
		}else {
			for (var i = 0; i < num; i++) {
				$('.sec-nav').eq(i).slideUp();
			}
		}
	});
	$('.header').on('mouseleave', function () {
		$('.sec-nav').slideUp(function () {
			setTimeout(function () {
				if(top == 0) {
					$('.header').css({'background': 'none'})
				}
				$('.header').css({'border-bottom': '0'});
			}, 300)
		});
	})
}
function navHover1 () {
	var top = 0;
	$(window).scroll(function () {
		top = $(this).scrollTop();
	});

	$('.nav-list > li').on('mouseover', function () {
		if(top == 0) {
			$('.header').css({'background': '#fff'})
		}
		$('.header').css({'border-bottom': '1px solid #e9eaea'});
		var findRes = $(this).find('.sec-nav').length;
		var num = $('.sec-nav').length;
		if (findRes > 0) {
			for (var i = 0; i < num; i++) {
				$('.sec-nav').eq(i).slideUp();
			}
			if ($('.sec-nav').is(':visible')) {
				$(this).find('.sec-nav').stop().show();
			} else  {
				$(this).find('.sec-nav').slideDown();
			}
		}else {
			for (var i = 0; i < num; i++) {
				$('.sec-nav').eq(i).slideUp();
			}
		}
	});
	$('.header').on('mouseleave', function () {
		$('.sec-nav').slideUp(function () {
			setTimeout(function () {
				if(top == 0) {
					$('.header').css({'background': 'none'})
				}
				$('.header').css({'border-bottom': '0'});
			}, 300)
		});
	})
}

// 首页 工程案例
function caseHover () {
	$('.case-list').on('mouseover', 'li', function () {
		$(this).find('.case-hover').removeClass('moveOut').addClass('moveIn')
	} ).on('mouseleave', 'li', function () {
		$(this).find('.case-hover').removeClass('moveIn').addClass('moveOut')
	})
}

// 首页新品上市
function newPro() {
	// 显示右边的详情图片
	var index = 0;
	$('.npr-r-box').eq(0).show();

	// 判断有几个三个产品一列
	var line = $('.npr-l-list .npr-l-box').length;

	// 每个产品的点击
	$(document).on('click', '.nprl-pic-box', function() {
		var picI = $(this).index();
		index = ($(this).parent('.npr-l-list').index() * 3) + picI;
		active($('.nprl-pic-box'), 'new-pro-on', picI);
		var num = $('.npr-r-box').length;
		for(var i=0; i<num; i++) {
			$('.npr-r-box').eq(i).hide();
		}
		$('.npr-r-box').eq(index).show();
		wowAni();
	});

	// 左右切换
	var click = 0;
	var animated = 0;
	$(document).on('click', '.npr-prev', function () {
		if(click == 0) {
			return;
		}
		// 判断大的box的是不是停留在初始的位置
		left = $('.npr-l-list')[0].offsetLeft;
		if(animated == 0) {
			animated = 1;
			if(top != 0) {
				click = (--click < 0)?0:click;
				$('.npr-l-list').animate({'left': -(click * 240) + 'px'}, 500, function() {
					animated = 0;
				});
				proOn(click);
			}
		}
	});
	$(document).on('click', '.npr-next', function() {
		if(click == (line - 1)) {
			return;
		}
		if(animated == 0) {
			animated = 1;
			if(line > 1) {
				click = (++click > (line - 1))?(line - 1):click;
				$('.npr-l-list').animate({'left': -(click * 240) + 'px'}, 500, function() {
					animated = 0;
				});
				// 图片切换的时候，on对象跟着变，右边的图片详情页跟着变
				proOn(click);
			}
		}
	});
}
// 左右切换的时候，右边图片自动定位到相对应的产品
function proOn(aIndex) {
	var num = $('.nprl-pic-box').length;
	for(var i=0; i<num; i++) {
		$('.nprl-pic-box').eq(i).removeClass('new-pro-on');
		$('.npr-r-box').eq(i).hide();
	}
	$('.npr-l-box').eq(aIndex).find('.nprl-pic-box').eq(0).addClass('new-pro-on');
	index =	aIndex * 3;
	$('.npr-r-box').eq(index).show();
	wowAni();
}
// 重新调用动画
function wowAni() {
	if (!(/msie [6|7|8|9]/i.test(navigator.userAgent))) {
		var wow = new WOW({
		    boxClass: 'npr-ani',
		    animateClass: 'animated',
		    offset: 0,
		    mobile: true,
		    live: true
		});
		wow.init()
	}
}

// 检测浏览器是否在ie8或者ie9下，显示输入框的提示
function labelShow(formEle) {
	if(navigator.userAgent.indexOf("MSIE 8.0")>0 || navigator.userAgent.indexOf("MSIE 9.0")>0) {
		console.log('ie');
		formEle.find('label').css({'display':'block'});
		formEle.find('label').on('click', function() {
			$(this).parent('form').find('input').focus();
		});
		formEle.find('input').on('keyup', function() {
			if($(this).val().length == 0) {
				$(this).parent('form').find('label').css({'display':'block'});
			}else {
				$(this).parent('form').find('label').css({'display':'none'});
			}
		});
	}
}

// 人才招聘 动画
function job() {
	$('.job-detail').stop().slideDown('slow');
}
function jobClose() {
	$('.job-detail').stop().slideUp('slow');
}

// 问卷调查
function btn() {
	$('.check-box a').attr('data-click',0);
	$('.qt').on('click', 'a', function() {
		var index = $(this).parent('p').parent('dd').parent('dl').index();
		var type = $(this).parent('p').parent('dd').attr('class');
		switch(type) {
			case 'radio':
				num = $(this).parent('p').parent('dd').parent('dl').find('a').length;
				for(var i=0; i<num; i++) {
					$('.qt').eq(index).find('a').eq(i).removeClass('radio-click');
				}
				$(this).addClass('radio-click');
			break;
			case 'check-box':
				if($(this).attr('data-click') == 0) {
					$(this).addClass('cb-click').attr('data-click',1);
				}else {
					$(this).removeClass('cb-click').attr('data-click',0);
				}
			break;
		}
	});
}
function cj() {
	var check = false;
	var num = $('.qt').length;
	var qtNum = [];
	for(var i=0; i<num; i++) {
		var type = $('.qt').eq(i).find('dd').attr('class');
		switch(type) {
			case 'radio':
				length = $('.qt').eq(i).find('.radio-click').length;
				if(length == 1) {
					check = true;
				}else {
					check = false;
				}
			break;
			case 'check-box':
				length = $('.qt').eq(i).find('.cb-click').length;
				if(length >= 1) {
					check = true;
				}else {
					check = false;
				}
			break;
			case 'num':
				var val = $('.num input').val();
				if(val != '' && isNaN(val) == false) {
					check = true;
				}else {
					check = false;
				}
			break;
		}
		qtNum.push({'num':i,'res':check});
		console.log(qtNum[i]);
	}
	forNum = 0;
	for(var i=0; i<num; i++) {
		if(qtNum[i].res == false) {
			forNum++;
			alert('请填写完整');
			return;
		}
	}
	if(forNum == 0) {
		$('.choujiang').stop().slideDown('slow');
		$(window).scrollTop(600);
	}
}
function cjClose() {
	$('.choujiang').stop().slideUp('slow');
}

/**
 * 回到顶部
 */
function backTop() {
	top = $(window).scrollTop();
	if(top != 0) {
		$('html,body').animate({scrollTop: 0},300);
	}
}
function showBackTop() {
	var top = 0;
	$(window).scroll(function() {
		top = $(this).scrollTop();
		if(top > 200) {
			$('.backTop-box').show();
		}else {
			$('.backTop-box').hide();
		}
	});
}

// 新闻列表切换
function newsTab(index) {
	var num = $('.news-tab li').length;
	for(var i=0; i<num; i++) {
		$('.news-list-box').eq(i).hide();
	}
	$('.news-list-box').eq(index).show();
	active($('.news-tab li'), 'news-tab-on', index);
}

// 下拉，上移动画
function show(ele, index) {
	var num = ele.length
	for(var i=0; i<num; i++) {
		ele.eq(i).stop().slideUp('slow')
	}
	ele.eq(index).stop().slideDown('slow')
}

// 当前active
function active (ele, active, index) {
	var num = ele.length
	for (var i=0; i<num; i++) {
		ele.eq(i).removeClass(active)
	}
	ele.eq(index).addClass(active)
}

function showTan(){
	$(".viap_tan").animate({top:'0px'});
	$(".viap_tan_content").animate({top:'50%'});
	}
function closeTan(){
	$(".viap_tan").animate({top:'-100%'});
	$(".viap_tan_content").animate({top:'-100%'});
	}

$(document).ready(function () {
    var temp = 1;
    var n = $(".header_daohan ul li").length;
    var m = n - 7;
    if (n > 7) {
        for (var j = 0; j < m; j++) {
            $(".header_daohan ul li").eq(7 + j).hide();
        }
        $(".header_menu").click(function () {
            if (temp == 1) {
                for (var j = 0; j < m; j++) {
                    $(".header_daohan ul li").eq(j).hide();
                }
                for (var j = 0; j < m; j++) {
                    $(".header_daohan ul li").eq(7 + j).show();
                }

                temp = 2;
            } else {
                for (var j = 0; j < m; j++) {
                    $(".header_daohan ul li").eq(j).show();
                }
                for (var j = 0; j < m; j++) {
                    $(".header_daohan ul li").eq(7 + j).hide();
                }
                temp = 1;
            }
        });
    }
});

function topSearch() {
    var key = $("#key").val();
    if (!key || key == "") {
        alert('请输入搜索关键字');
        return false;
    }
    return true;

}

$(function () {
    $('.header_search').click(function () {
        $('.search_form').toggleClass('open');
        return false;
    });
})

function doPostFooterMessage() {
    var name = $("#name").val();
    var phone = $("#phone").val();
    var content = $("#content").val();
    if (!name) {
        alert('请输入您的姓名！');
        return false;
    }
    if (!phone) {
        alert('请输入您的电话号码！');
        return false;
    }
    if (!content) {
        alert('请填写您的留言内容！');
        return false;
    }
    var strQueryString = $("#footerMessageForm").formSerialize();
    var loading = layer.load(0, { shade: [0.2, '#000'] });
    $.ajax({
        type: "POST",
        url: "/Server/DoPostMessage",
        data: strQueryString,
        dataType: "JSON",
        success: function (data) {
            layer.close(loading);
            if (data.status == "success") {
                layer.msg(data.message, {
                    time: 2000 //2秒关闭（如果不配置，默认是3秒）
                }, function () {
                    window.location.href = window.location.href;
                });
            }
            else {
                alert(data.message);
            }
        },
        error: function () {
            console.log('执行出错！');
            layer.close(loading);
            alert('执行错误，请联系管理员！');
        }
    });
    return false;
}

function doPostMessage() {
    var name = $("#name_txt").val();
    var phone = $("#phone_txt").val();
    var content = $("#content_txt").val();
	var __RequestVerificationToken = $("intput[name='__RequestVerificationToken']").val();
    if (!name) {
        alert('请输入您的姓名！');
        return false;
    }
    if (!phone) {
        alert('请输入您的电话号码！');
        return false;
    }
    if (!content) {
        alert('请填写您的留言内容！');
        return false;
    }
    var postData = {
        name: name,
        phone: phone,
        content: content,
		__RequestVerificationToken: __RequestVerificationToken
    };
    var loading = layer.load(0, { shade: [0.2, '#000'] });
    $.ajax({
        type: "POST",
        url: "/Server/DoPostMessage",
        data: postData,
        dataType: "JSON",
        success: function (data) {
            layer.close(loading);
            if (data.status == "success") {
                layer.msg(data.message, {
                    time: 2000 //2秒关闭（如果不配置，默认是3秒）
                }, function () {
                    window.location.href = window.location.href;
                });
            }
            else {
                alert(data.message);
            }
        },
        error: function () {
            console.log('执行出错！');
            layer.close(loading);
            alert('执行错误，请联系管理员！');
        }
    });
    return false;
}
var menu_show = 0;
function mbNav() {
    if (menu_show === 0) {
        $('.menu-btn').addClass('menu-on');
        $('.menu-box').slideDown('slow');
        menu_show = 1;
    } else {
        $('.menu-box').slideUp('slow', function () {
            $('.menu-btn').removeClass('menu-on');
        });
        menu_show = 0;
    }
}