// <![CDATA[
(function ($) {
    $.fn.InfiniteScroll = function (options) {
        var defaults = {
            moreInfoDiv: '#MoreInfoDiv',
            progressDiv: '#Progress',
            loadInfoUrl: '/',
            loginUrl: '/login',
            errorHandler: null,
            completeHandler: null,
            noMoreInfoHandler: null
        };
        var options = $.extend(defaults, options);

        var showProgress = function () {
            $(options.progressDiv).css("display", "block");
        }

        var hideProgress = function () {
            $(options.progressDiv).css("display", "none");
        }       

        return this.each(function () {
            var moreInfoButton = $(this);
            var page = 1;
            $(moreInfoButton).click(function (event) {
                showProgress();
                $.ajax({
                    type: "POST",
                    url: options.loadInfoUrl,
                    data: JSON.stringify({ page: page }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    complete: function (xhr, status) {
                        var data = xhr.responseText;
                        if (xhr.status == 403) {
                            window.location = options.loginUrl;
                        }
                        else if (status === 'error' || !data) {
                            if (options.errorHandler)
                                options.errorHandler(this);
                        }
                        else {
                            if (data == "no-more-info") {
                                if (options.noMoreInfoHandler)
                                    options.noMoreInfoHandler(this);
                            }
                            else {
                                var $boxes = $(data);
                                $(options.moreInfoDiv).append($boxes);
                            }
                            page++;
                        }
                        hideProgress();
                        if (options.completeHandler)
                            options.completeHandler(this);
                    }
                });
            });
        });
    };
})(jQuery);
// ]]>