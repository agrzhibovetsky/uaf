<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Photo.aspx.cs" Inherits="UaFootball.WebApplication.Public.Photo" %>
<html>
    <head>
        <script type="text/javascript" src="../Scripts/jssor.slider.js"></script>
        <script type="text/javascript" src="../Scripts/jssor.js"></script>
       
    </head>
    <body>
    <div style="height: 750px; width:100%;  ">
    <div id="caption1" style="height:20px; position:relative; left:60px; top:10px; width:700px;"></div>
    <div id="slider1_container" style="position: relative; top: 10px; left: 0px; width: 800px; height: 610px; ">
    
        <div u="slides" style="cursor: move; position: absolute; overflow: hidden; left: 60px; top: 0px; width: 800px; height: 590px;">
            <asp:Repeater ID="rptJssorContent" runat="server">
                <ItemTemplate>
                    <div>
                        <img alt="" u="image" src2='<%#Eval("filePath")%>' id='slide<%# Container.ItemIndex %>' title='<%#Eval("title1")%>' title2='<%#Eval("title2")%>' /> 
                        <img alt="" u="thumb" src2='<%#Eval("fileThumbPath")%>' />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            
        </div>
        <div id="caption2" style="position: absolute; top:590px; left: 60px; width:800px; height: 20px; text-align:right;"></div>
        <div u="thumbnavigator" class="jssort01" style="position: absolute; width: 800px; height: 100px; left:60px; top: 620px; ">
        
            <!-- Thumbnail Item Skin Begin -->
            <style>
                /* jssor slider thumbnail navigator skin 01 css */
                /*
                .jssort01 .p           (normal)
                .jssort01 .p:hover     (normal mouseover)
                .jssort01 .pav           (active)
                .jssort01 .pav:hover     (active mouseover)
                .jssort01 .pdn           (mousedown)
                */
                .jssort01 .w
                {
                    position: absolute;
                    top: 0px;
                    left: 0px;
                    width: 100%;
                    height: 100%;
                    text-align: center;
                }
                .jssort01 .c {
                    position: absolute;
                    top: 0px;
                    left: 0px;
                    width: 121px;
                    height: 96px;
                    
                }
                .jssort01 .p:hover .c, .jssort01 .pav:hover .c, .jssort01 .pav .c {
                    
                    border-width: 0px;
                    top: 2px;
                    left: 2px;
                    width: 121px;
                    height: 96px;
                }
                .jssort01 .p:hover .c, .jssort01 .pav:hover .c {
                    top: 0px;
                    left: 0px;
                    width: 123px;
                    height: 98px;
                    border: #fff 1px solid;
                }
            </style>
            <div u="slides" style="cursor: move;">
                <div u="prototype" class="p" style="position: absolute; width: 125px; height: 100px; top: 0; left: 0;">
                    <div class=w><thumbnailtemplate style="height: 100px; border: #000 2px solid; margin-left:auto; margin-right:auto;"></thumbnailtemplate></div>
                    <div class=c>
                    </div>
                </div>
            </div>
            <!-- Thumbnail Item Skin End -->
        </div>
        <!-- Thumbnail Navigator Skin End -->

    </div>
    </div>
<script type="text/javascript">

    function setCaptions(slideIndex) {
      
        var div_caption1 = $("#caption1");
        var div_caption2 = $("#caption2");
        var cur_img = $("#slide" + slideIndex);
        div_caption1.html(cur_img.attr("title"));
        div_caption2.html(cur_img.attr("title2"));
    }



    $(document).ready(function () {

        var _CaptionTransitions = [];

        var options = { $AutoPlay: false,
            $FillMode: 1,
            $LazyLoading: 2,
            $ThumbnailNavigatorOptions:
                            {
                                $Class: $JssorThumbnailNavigator$,
                                $ChanceToShow: 2,
                                $ActionMode: 1,
                                $SpacingX: 8,
                                $DisplayPieces: 6,
                                $ParkingPosition: 360
                            }
        };
        var jssor_slider1 = new $JssorSlider$('slider1_container', options);
        setCaptions(0);
        jssor_slider1.$On($JssorSlider$.$EVT_PARK, function (slideIndex, fromIndex) {
            setCaptions(slideIndex);
        });
    });
    </script>
    </body>
</html>