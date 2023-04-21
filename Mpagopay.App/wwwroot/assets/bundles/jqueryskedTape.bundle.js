!function(n){"function"==typeof define&&define.amd?define(["jquery"],n):"object"==typeof module&&module.exports?module.exports=function(t,e){return void 0===e&&(e="undefined"!=typeof window?require("jquery"):require("jquery")(t)),n(e),e}:n(jQuery)}(function(h){function a(t){h.extend(this,t),this.$el=t&&t.el?h(t.el):h("<div/>"),this.el=t.el instanceof h?t.el[0]:t.el,this.locations=[],this.events=[],this.lastEventId=0,this.format=h.extend({},a.defaultFormatters,t&&t.formatters||{}),this.$el.on("click",".sked-tape__event",h.proxy(this.handleEventClick,this)),this.$el.on("contextmenu",".sked-tape__event",h.proxy(this.handleEventContextMenu,this)),this.$el.on("click",".sked-tape__timeline-wrap",h.proxy(this.handleTimelineClick,this)),this.$el.on("contextmenu",".sked-tape__timeline-wrap",h.proxy(this.handleTimelineContextMenu,this)),this.$el.on("mousemove",".sked-tape__timeline-wrap",h.proxy(this.handleMouseMove,this)),this.$el.on("keydown",".sked-tape__time-frame",h.proxy(this.handleKeyDown,this)),this.$el.on("wheel",".sked-tape__time-frame",h.proxy(this.handleWheel,this)),this.$el.on("click",".sked-tape__intersection",h.proxy(this.handleIntersectionClick,this)),this.$el.on("contextmenu",".sked-tape__intersection",h.proxy(this.handleIntersectionContextMenu,this))}var o=(new Date).getTimezoneOffset(),e=(a.defaultFormatters={date:function(t,e,n){e=e||"m",n=n||("m"===e?"/":".");t=[t.getDate(),t.getMonth()+1,t.getFullYear()];return(t="m"===e?[t[1],t[0],t[2]]:t).join(n)},roundDuration:function(t){return t},duration:function(t,e){var n=Math.floor(t/s),t=Math.floor(t%s/r),i=e&&e.hrs||"hrs",e=e&&e.min||"min",i=n?n+i:"";return i+(n&&t?" ":"")+(t?t+e:"")},hours:function(t){return(t<10?"0":"")+t+":00"},time:function(t){var e=t.getHours(),t=t.getMinutes();return(e<10?"0"+e:e)+":"+(t<10?"0"+t:t)}},a.prototype={constructor:a,setTimespan:function(t,e,n){if(l(t,e))return this.start=p(t),this.end=function(t){var e=p(t);e<t&&e.setTime(e.getTime()+s);return e}(e),this.updateUnlessOption(n);throw new Error("Invalid time range: "+JSON.stringify([t,e]))},setDate:function(t,e,n){var i,t=new Date(t),s=(t.setHours(0,0,0,0),new Date(t));return s.setHours(e||0),n&&24!=n?(i=new Date(t.getTime())).setHours(n):i=new Date(t.getTime()+d),this.setTimespan(s,i)},getZoom:function(){return this.zoom},setZoom:function(t){return(t=t||1)<1?this.zoom=1:t>this.maxZoom?this.zoom=this.maxZoom:(this.zoom=t,this.$canvas&&(t=this.$canvas.data("orig-min-width")*t,this.$canvas.css("min-width",Math.round(t)+"px"))),this},resetZoom:function(){return this.setZoom()},zoomIn:function(t){return this.setZoom(this.zoom+(t||this.zoomStep))},zoomOut:function(t){return this.setZoom(this.zoom-(t||this.zoomStep))},locationExists:function(n){var i=!1;return h.each(this.locations,function(t,e){if(e.id==n)return!(i=!0)}),i},setLocations:function(t,e){return this.events=[],this.locations=t&&t.map(function(t){return{id:t.id,name:t.name,order:t.order||0,tzOffset:t.tzOffset,userData:t.userData?h.extend({},t.userData):{}}}),this.updateUnlessOption(e)},addLocations:function(t,e){return this.locations=this.locations.concat(t),this.updateUnlessOption(e)},addLocation:function(t,e){return this.locations.push(t),this.updateUnlessOption(e)},removeLocation:function(t,e){for(var n=this.events.length-1;0<=n;--n)this.events[n].location==t&&this.events.splice(n,1);for(n=0;n<this.locations.length;++n)if(this.locations[n].id==t){this.locations.splice(n,1);break}return this.updateUnlessOption(e)},getLocation:function(t){for(var e=0;e<this.locations.length;++e)if(this.locations[e].id==t)return this.locations[e];return null},getLocations:function(){var t=this.locations;return this.sorting&&"name"===this.orderBy?t=t.sort(function(t,e){return t=t.name.toLocaleLowerCase(),e=e.name.toLocaleLowerCase(),t.localeCompare(e)}):this.sorting&&"order"===this.orderBy&&(t=t.sort(function(t,e){return(t.order||0)-(e.order||0)})),t},collide:function(t){for(var e,n,i,s=0;s<this.events.length;++s)if(t.location==this.events[s].location&&(e=t,n=this.events[s],i=void 0,((i=e.start<n.start?e:n)===e?n:e).start-i.end<this.minGapTimeBetween))return this.events[s];return null},addEvent:function(t,e){if(!this.locationExists(t.location))throw new Error("Unknown location #"+t.location);var n=t.start instanceof Date?t.start:new Date(t.start),i=t.end instanceof Date?t.end:new Date(t.end);if(!l(n,i))throw new Error("Invalid time range: "+JSON.stringify([t.start,t.end]));n={id:++this.lastEventId,name:t.name,location:t.location+"",start:n,end:i,data:t.data?h.extend({},t.data):null,url:t.url||!1,className:t.className||null,disabled:t.disabled||!1,active:t.active||!1,userData:h.extend({},t.userData||{})};if(e&&e.preserveId&&t.id){if(this.getEvent(t.id))throw new Error("Cannot preserve id: already exists");n.id=t.id}else n.id=++this.lastEventId;if(!e||!e.allowCollisions){i=this.collide(n);if(i)throw new a.CollisionError(i.id)}return this.events.push(n),this.updateUnlessOption(e),n},addEvents:function(t,e){return t.forEach(function(t){this.addEvent(t,h.extend({},{update:!1},e))},this),this.updateUnlessOption(e)},setEvents:function(t,e){return this.removeAllEvents(e).addEvents(t,e)},removeEvent:function(n,t){return h.each(this.events,h.proxy(function(t,e){if(e.id==n)return this.events.splice(t,1),!1},this)),this.updateUnlessOption(t)},removeAllEvents:function(t){return this.$el.find(".sked-tape__event, .sked-tape__gap").remove(),this.events=[],this.updateUnlessOption(t)},getEvents:function(){return this.events},getEvent:function(n){var i=null;return h.each(this.events,h.proxy(function(t,e){if(e.id==n)return i=e,!1},this)),i},isEditMode:function(){return this.editMode},enterEditMode:function(){return this.editMode=!0,this},leaveEditMode:function(){return this.isAdding()&&this.cancelAdding(),this.editMode=!1,this},startAdding:function(t){return this.dummyEvent=t,this.lastPicked&&(this.moveDummyEvent(this.lastPicked),this.updateDummyEvent()),this.rerenderLocations()},cancelAdding:function(){var t,e;return this.dummyEvent&&((t=this.dummyEvent.draggedEvent)&&(this.addEvent(t,{preserveId:!0,update:!0,allowCollisions:!0}),e=h.Event("event:dragCanceled.skedtape",{detail:{event:t}}),this.$el.trigger(e,t)),delete this.dummyEvent),this.$dummyEvent&&(this.$dummyEvent.remove(),delete this.$dummyEvent,this.$el.trigger("event:addingCanceled.skedtape")),this.rerenderLocations()},isAdding:function(){return!!this.dummyEvent},rerenderLocation:function(t){var e=this.$locations.filter(function(){return h(this).data("id")==t}),n=this.getLocation(t);e.length&&n&&e.replaceWith(this.renderLocation(n))},rerenderLocations:function(){return this.$locations.empty().append(this.renderLocations()),this},renderLocation:function(t){var e=h('<div class="sked-tape__location-text"/>').text(t.name),n=h('<li class="sked-tape__location"/>').attr({title:t.name,"data-id":t.id}).append(e),i=this.isAdding()?this.canAddIntoLocation(t,this.dummyEvent):void 0;return this.postRenderLocation(e,t,i),n},renderLocations:function(){var n=h(document.createDocumentFragment());return h.each(this.getLocations(),h.proxy(function(t,e){this.renderLocation(e).appendTo(n)},this)),n},postRenderLocation:function(t,e,n){void 0!==n&&t.parent().toggleClass("sked-tape__location--permitted",n).toggleClass("sked-tape__location--forbidden",!n)},renderAside:function(){var t=h('<div class="sked-tape__aside"/>');h('<div class="sked-tape__caption"/>').text(this.caption).appendTo(t),this.$locations=h('<ul class="sked-tape__locations"/>').append(this.renderLocations()).appendTo(t),this.$el.append(t)},renderTimeWrap:function(t){var e=this.renderHours(),n=h('<div class="sked-tape__time-wrap"/>').appendTo(this.$el),n=(this.$frame=h('<div class="sked-tape__time-frame" tabindex="0"/>').appendTo(n),this.$canvas=h('<div class="sked-tape__time-canvas"/>').append(e).appendTo(this.$frame),t&&this.$frame.scrollLeft(t),h('<div class="sked-tape__timeline-wrap"/>').append(this.renderTimeRows()).append(this.renderGrid())),t=this.$canvas[0].scrollWidth;this.$canvas.css("min-width",Math.round(t*this.zoom)+"px").data("orig-min-width",t).append(n).append(e.clone()),this.showDates&&this.$canvas.prepend(this.renderDates())},renderDates:function(){var t,e,n=h('<ul class="sked-tape__dates"/>'),i=(e=this.start,(e=new Date(e)).setTime(e.getTime()+m(e)),t=this.end,(t=new Date(t)).setTime(t.getTime()-u(t)),t),s=[];if(i<e)s.push({weight:1,text:this.format.date(this.start)});else{s.push({weight:m(this.start)/d,text:this.format.date(this.start)});for(var a=new Date(e);a<i;)a.setTime(a.getTime()+1e3),s.push({weight:1,text:this.format.date(a)}),a.setTime(a.getTime()+d-1e3);s.push({weight:u(this.end)/d,text:this.format.date(this.end)})}var o=s.reduce(function(t,e){return t+e.weight},0),r=this.end.getTime()-this.start.getTime();return s.forEach(function(t){var e=t.weight/o;h("<li/>").css("width",(100*e).toFixed(10)+"%").attr("title",t.text).addClass("sked-tape__date").toggleClass("sked-tape__date--short",e*r<=c).appendTo(n)}),n},renderHours:function(){for(var t=h("<ul/>"),e=new Date(this.start);e.getTime()<=this.end.getTime();){var n=e.getHours(),n=h("<time/>").attr("datetime",e.toISOString()).text(this.format.hours(24===n?0:n));h("<li/>").append(n).appendTo(t),e.setTime(e.getTime()+36e5)}var i=t.children();return i.not(":last-child").width(100/(i.length-1)+"%"),h('<div class="sked-tape__hours"/>').append(t)},renderGrid:function(){for(var t=h('<ul class="sked-tape__grid"/>'),e=new Date(this.start);e.getTime()<this.end.getTime();)h("<li/>").appendTo(t),e.setTime(e.getTime()+36e5);var n=t.children();return n.width(100/n.length+"%"),t},renderTimeRows:function(){this.$timeline=h('<ul class="sked-tape__timeline"/>');var n=this.events.sort(h.proxy(function(t,e){return t.start.getTime()-e.start.getTime()},this));return this.timeIndicators={},h.each(this.getLocations(),h.proxy(function(t,s){var a,o=h('<li class="sked-tape__event-row"/>').data("locationId",s.id).appendTo(this.$timeline),e=h('<div class="sked-tape__indicator"/>').hide(),r=(this.timeIndicatorSerifs&&e.addClass("sked-tape__indicator--serifs"),this.timeIndicators[s.id]=e,o.append(e),this.getIntersections(s.id)),d=0;n.forEach(function(n){var i,t=n.location==s.id,e=n.end>this.start&&n.start<this.end;t&&e&&(i=!1,h.each(r,h.proxy(function(t,e){if(h.each(e.events,function(t,e){if(e.id==n.id)return!(i=!0)}),i)return!1},this)),(t=n.start.getTime()-d)>=this.minTimeGapShown&&t<=this.maxTimeGapShown&&!i&&o.append(this.renderGap(t,a,n.start)),a=n.end,d=a.getTime(),e=this.renderEvent(n).appendTo(o),!1!==this.maxTimeGapHi&&t<=this.maxTimeGapHi?o.children(".sked-tape__event").filter(":eq(-1), :eq(-2)").addClass("sked-tape__event--low-gap"):i&&e.addClass("sked-tape__event--low-gap"))},this)},this)),this.renderIntersections(),this.$timeline},renderIntersections:function(){this.$timeline.find(".sked-tape__intersection").remove(),this.$timeline.find(".sked-tape__event-row").each(h.proxy(function(t,e){var n=h(e),e=n.data("locationId"),e=this.getIntersections(e);h.each(e,h.proxy(function(t,e){e.end>this.start&&e.start<this.end&&h('<div class="sked-tape__intersection"/>').css({width:this.computeEventWidth(e),left:this.computeEventOffset(e)}).data("events",e.events).appendTo(n)},this))},this))},renderGap:function(t,e,n){e={start:e,end:n},n=h('<span class="sked-tape__gap-text"/>').text(Math.round(t/r));return h('<div class="sked-tape__gap"/>').css({width:this.computeEventWidth(e),left:this.computeEventOffset(e)}).append(n)},findEventJustBefore:function(n){var i=null;return h.each(this.events,function(t,e){e.location==n.location&&e.end<n.start&&(!i||i.end<e.end)&&(i=e)}),i},findEventJustAfter:function(n){var i=null;return h.each(this.events,function(t,e){e.location==n.location&&e.start>n.end&&(!i||i.start>e.start)&&(i=e)}),i},updateDummyEvent:function(){var t,e,n,i,s,a,o;this.isAdding()?(t=this.dummyEvent,this.$dummyEvent||(this.$dummyEvent=h("<div/>").append('<div class="sked-tape__dummy-event-time sked-tape__dummy-event-time--left"/>').append('<div class="sked-tape__dummy-event-time sked-tape__dummy-event-time--right"/>')),e=(n=this.$dummyEvent.children()).filter(":first"),n=n.filter(":last"),this.$dummyEvent[0].className="sked-tape__dummy-event "+(t.className||""),this.$dummyEvent.css({width:this.computeEventWidth(t),left:this.computeEventOffset(t)}),i=this.format.time(t.start),s=this.format.time(t.end),this.showIntermission&&((o=this.findEventJustBefore(t))&&(a=Math.round((t.start-o.end)/r))>=this.intermissionRange[0]&&a<=this.intermissionRange[1]&&(i+="<br>+"+this.format.duration(a*r)),o=this.findEventJustAfter(t))&&(a=Math.round((o.start-t.end)/r))>=this.intermissionRange[0]&&a<=this.intermissionRange[1]&&(s+="<br>+"+this.format.duration(a*r)),e.html(i),n.html(s),(o=this.$dummyEvent.closest(".sked-tape__event-row")).length&&o.data("locationId")==t.location||(this.$dummyEvent.remove(),(o=this.$el.find(".sked-tape__event-row").filter(function(){return h(this).data("locationId")==t.location})).length&&this.$dummyEvent.appendTo(o))):this.$dummyEvent&&(this.$dummyEvent.remove(),delete this.$dummyEvent)},updateEvent:function(t){var e=this.getEvent(t),n=this.$timeline.find(".sked-tape__event").filter(function(){return h(this).data("eventId")==t});e&&n.length?(e=this.renderEvent(e),n.replaceWith(e)):this.update()},renderEvent:function(t){(e=t.url&&!t.disabled?h("<a/>").attr("href",t.url):h("<div/>")).addClass("sked-tape__event"),t.className&&e.addClass(t.className),e.toggleClass("sked-tape__event--disabled",!!t.disabled).toggleClass("sked-tape__event--active",!!t.active).attr("title",t.name).css({width:this.computeEventWidth(t),left:this.computeEventOffset(t)});var e,n,i=h('<div class="sked-tape__center"/>').text(t.name).appendTo(e),s=((this.showEventTime||this.showEventDuration)&&(n=i.html(),s=this.format.roundDuration(t.end-t.start),this.showEventTime&&(n+="<br>"+this.format.time(t.start)+" - "+this.format.time(new Date(t.start.getTime()+s))),this.showEventDuration&&(n+="<br>"+this.format.duration(s)),i.html(n)),e.data(h.extend({},{eventId:t.id},t.data)),e.clone().css({width:"",left:"-10000px",top:"-10000px"}).appendTo(document.body));return e.data("min-width",s.outerWidth()),s.remove(),this.postRenderEvent(e,t),e},computeEventWidth:function(t){var e=(this.end<t.end?this:t).end;return i(t.start,e)/i(this.start,this.end)*100+"%"},computeEventOffset:function(t){return i(this.start,t.start)/i(this.start,this.end)*100+"%"},updateTimeIndicatorsPos:function(){var n=this.start.getTime(),i=this.end.getTime(),s=(new Date).getTime();Object.keys(this.timeIndicators).forEach(function(t){var e=this.getLocation(t),e=(void 0===e.tzOffset?this:e).tzOffset,e=s-(e-o)*r,t=this.timeIndicators[t];n<=e&&e<=i?(e=100*(e-n)/(i-n)+"%",t.show().css("left",e)):t.hide()},this)},hasIntersections:function(){for(var t,e,n,i=0;i<this.events.length;i++)for(t=this.events[i],n=i+1;n<this.events.length;n++)if(e=this.events[n],t.location===e.location&&v(t,e))return!0;return!1},getIntersections:function(a){var o=[];return h.each(this.events,h.proxy(function(t,e){if(e.location==a)for(var n=t+1;n<this.events.length;++n){var i,s=this.events[n];s.location==a&&((i=v(e,s))&&!function(t){for(var e=0;e<o.length;++e)if(t.start.getTime()==o[e].start.getTime()&&t.end.getTime()==o[e].end.getTime())return!0;return!1}(i))&&(i.events=[e,s],o.push(i))}},this)),o},destroy:function(){this.cleanup(),this.$el.off().empty().removeClass("sked-tape sked-tape--has-dates")},cleanup:function(){h.fn.popover&&this.$el.find(".sked-tape__event").popover(4<=e?"dispose":"destroy"),this.indicatorTimeout&&(clearInterval(this.indicatorTimeout),delete this.indicatorTimeout)},render:function(t){t=t&&t.preserveScroll&&this.$frame&&this.$frame.scrollLeft();return this.cleanup(),this.$el.empty().addClass("sked-tape"),this.showDates&&this.$el.addClass("sked-tape--has-dates"),this.renderAside(),this.renderTimeWrap(t),this.updateTimeIndicatorsPos(),this.indicatorTimeout=setInterval(h.proxy(function(){this.updateTimeIndicatorsPos()},this),1e3),setTimeout(h.proxy(function(){var a='<div class="popover" role="tooltip"><div class="arrow"></div><div class="popover-'+(4<=e?"body":"content")+'"></div></div>';this.$el.find(".sked-tape__event").each(h.proxy(function(t,e){var e=h(e),n=e.width()<e.data("min-width"),i=parseFloat(e[0].style.left),s=i+parseFloat(e[0].style.width);h.fn.popover&&"never"!==this.showPopovers&&(n||(i<-.01||100.01<s)||"always"===this.showPopovers)&&e.popover({trigger:"hover",content:e.find(".sked-tape__center").html(),html:!0,template:a,placement:i<50?"right":"left"})},this))},this),0),this},update:function(){return this.render({preserveScroll:!0})},updateUnlessOption:function(t){t=!t||void 0===t.update||t.update;return this.$timeline&&t?this.update():this},setSnapToMins:function(t){this.snapToMins=t},findEventsAtTime:function(t,n){var i=t.getTime(),s=[];return h.each(this.getEvents(),function(t,e){e.location==n&&e.start.getTime()<=i&&e.end.getTime()>=i&&s.push(e)}),s},pick:function(n){var i,t=(n.pageX-this.$timeline.offset().left)/this.$timeline.width(),t=this.start.getTime()+t*(this.end.getTime()-this.start.getTime());return this.$el.find(".sked-tape__event-row").each(function(){var t=h(this).offset().top,e=t+h(this).height();if(n.pageY>=t&&n.pageY<=e)return i=h(this).data("locationId"),!1}),{locationId:i,date:new Date(Math.round(t))}},makeMouseEvent:function(t,e,n){return h.Event(t,h.extend({},n,{relatedTarget:e.currentTarget,clientX:e.clientX,clientY:e.clientY,offsetX:e.offsetX,offsetY:e.offsetY,pageX:e.pageX,pageY:e.pageY,screenX:e.screenX,screenY:e.screenY,detail:h.extend(this.pick(e),n.detail)}))},dragEvent:function(t,e){var n,i;e=e||{},this.isAdding()||(n=this.getEvent(t),i=this.makeMouseEvent("event:dragStart.skedtape",e,{detail:{component:this,event:n}}),this.$el.trigger(i,[this]),i.isDefaultPrevented())||(i=this.makeMouseEvent("event:dragStarted.skedtape",e,{detail:{component:this,event:n}}),this.$el.trigger(i,[this]),this.removeEvent(t),this.startAdding({id:n.id,name:n.name,duration:n.end.getTime()-n.start.getTime(),userData:h.extend({},n.userData||{}),draggedEvent:n}))},handleEventClick:function(t){var e=h(t.currentTarget).data("eventId"),n=this.getEvent(e);this.isEditMode()?this.dragEvent(e,t):(e=this.makeMouseEvent("event:click.skedtape",t,{detail:{component:this,event:n}}),this.$el.trigger(e,[this]))},handleEventContextMenu:function(t){if(t.preventDefault(),this.rmbCancelsAdding&&this.isAdding())return this.cancelAdding();var e=h(t.currentTarget).data("eventId"),e=this.getEvent(e),t=this.makeMouseEvent("event:contextmenu.skedtape",t,{detail:{component:this,event:e}});this.$el.trigger(t,[this])},handleIntersectionClick:function(t){var t=this.makeMouseEvent("intersection:click.skedtape",t,{detail:{component:this}}),e=t.detail;e.events=this.findEventsAtTime(e.date,e.locationId),this.$el.trigger(t,[this])},handleIntersectionContextMenu:function(t){if(t.preventDefault(),this.rmbCancelsAdding&&this.isAdding())return this.cancelAdding();var t=this.makeMouseEvent("intersection:contextmenu.skedtape",t,{detail:{component:this}}),e=t.detail;e.events=this.findEventsAtTime(e.date,e.locationId),this.$el.trigger(t,[this])},completeAdding:function(t){var e=this.dummyEvent;if(this.collide(e))n=this.makeMouseEvent("event:dragEndRefused.skedtape",t,{detail:{component:this,event:e}}),this.$el.trigger(n,[this]);else{var n=this.makeMouseEvent("event:dragEnd.skedtape",t,{detail:{component:this,event:e}});if(this.$el.trigger(n,[this]),!n.isDefaultPrevented())try{var i=this.addEvent(e,{preserveId:!0,update:!0}),n=(delete e.duration,delete this.dummyEvent,this.$dummyEvent.remove(),delete this.$dummyEvent,this.makeMouseEvent("event:dragEnded.skedtape",t,{detail:{component:this,event:i}}));this.$el.trigger(n,[this]),this.rerenderLocations()}catch(t){if("SkedTape.CollisionError"!==t.name)throw t;n=this.makeMouseEvent("event:dragEndRefused.skedtape",t,{detail:{component:this,event:e}});this.$el.trigger(n,[this])}}},handleTimelineClick:function(t){n(t)||(this.isAdding()?this.dummyEvent.location&&this.completeAdding(t):(t=this.makeMouseEvent("timeline:click.skedtape",t,{detail:{component:this}}),this.$el.trigger(t,[this])))},handleTimelineContextMenu:function(t){if(!n(t)){if(t.preventDefault(),this.rmbCancelsAdding&&this.isAdding())return this.cancelAdding();t=this.makeMouseEvent("timeline:contextmenu.skedtape",t,{detail:{component:this}});this.$el.trigger(t,[this])}},moveDummyEvent:function(t){var e,n,i=this.dummyEvent,s=t.date;this.snapToMins&&(n=p(s),e=(e=(s.getTime()-n.getTime())/r)-(e=Math.floor(e/this.snapToMins)*this.snapToMins)<this.snapToMins/2?e:e+this.snapToMins,s=new Date(n.getTime()+Math.round(e*r))),h.extend(i,{start:s,end:new Date(s.getTime()+i.duration)}),t.locationId&&(n=this.getLocation(t.locationId),this.canAddIntoLocation(n,i))&&(this.beforeAddIntoLocation(n,i),i.location=t.locationId)},handleMouseMove:function(t){this.lastPicked=this.pick(t),this.isAdding()&&(this.moveDummyEvent(this.lastPicked),this.updateDummyEvent())},handleKeyDown:function(t){"+"===t.key?this.zoomIn():"-"===t.key&&this.zoomOut()},handleWheel:function(t){var e;return t.ctrlKey?(t.originalEvent.deltaY<0?this.zoomIn():this.zoomOut(),!1):!t.shiftKey&&this.scrollWithYWheel?(t=0<t.originalEvent.deltaY?1:-1,t*=.9*this.$frame.width(),this.$frame.queue().length&&(e=this.$frame.scrollLeft(),t+=this.$frame.finish().scrollLeft()-e,this.$frame.scrollLeft(e)),this.$frame.animate({scrollLeft:"+="+t},200),!1):void 0}},(a.CollisionError=function(t){this.message="Collision with entry #"+t,this.eventId=t,"captureStackTrace"in Error?Error.captureStackTrace(this,a.CollisionError):this.stack=(new Error).stack}).prototype=Object.create(Error.prototype),a.CollisionError.prototype.name="SkedTape.CollisionError",a.CollisionError.prototype.constructor=a.CollisionError,h.fn.popover?parseInt(h.fn.popover.Constructor.VERSION.charAt(0),10):0),d=864e5,r=6e4,s=60*r,c=2*s-1;function n(t){return h(t.target).closest(".sked-tape__event, .sked-tape__intersection").length}function l(t,e){return t instanceof Date&&e instanceof Date&&t<=e}function i(t,e){return(e.getTime()-t.getTime())/1e3/60/60}function u(t){return 1e3*(60*t.getHours()*60+60*t.getMinutes()+t.getSeconds())+t.getMilliseconds()}function m(t){return d-u(t)}function p(t){var e=new Date(t);return e.setHours(t.getHours(),0,0,0),e}function v(t,e){var n=t.start<e.start?t:e,e=n==t?e:t;return n.end<e.start?null:{start:e.start,end:(n.end<e.end?n:e).end}}h.fn.skedTape=function(n){var i=n&&("string"==typeof n||n instanceof String)?n:"",s=(n=n&&!i&&"object"==typeof n?h.extend({},n):{},i?Array.prototype.slice.call(arguments,1):[]);return this.each(function(){var t=h(this).data(h.fn.skedTape.dataKey);if(t&&i)if("destroy"===i)t.destroy(),h(this).removeData(h.fn.skedTape.dataKey);else{if(!(0<=["addEvent","addEvents","removeEvent","setEvents","removeAllEvents","enterEditMode","leaveEditMode","startAdding","cancelAdding","setLocations","addLocation","addLocations","removeLocation","setTimespan","setDate","zoomIn","zoomOut","setZoom","resetZoom","render","setSnapToMins","dragEvent","updateEvent"].indexOf(i)))throw new Error("SkedTape plugin cannot recognize command");t[i].apply(t,s)}else{if(i)throw new Error("SkedTape plugin hadn't been initialized but used");t&&t.destroy();var e=h.extend({},h.fn.skedTape.defaults,n,{el:this});delete e.locations,delete e.events,delete e.start,delete e.end,delete e.deferRender,t=new a(e),n.start&&n.end&&t.setTimespan(n.start,n.end,{update:!1}),n.locations&&t.setLocations(n.locations,{update:!1}),n.events&&t.setEvents(n.events,{update:!1,allowCollisions:!0}),h(this).data(h.fn.skedTape.dataKey,t),n.deferRender||t.render()}})},h.fn.skedTape.dataKey="sked-tape",h.fn.skedTape.format=a.defaultFormatters,h.fn.skedTape.defaults={caption:"",maxZoom:10,zoomStep:.5,zoom:1,showEventTime:!1,showEventDuration:!1,showDates:!0,minGapTimeBetween:0,minTimeGapShown:+r,maxTimeGapShown:60*r-1,maxTimeGapHi:!1,scrollWithYWheel:!1,sorting:!1,orderBy:"order",snapToMins:1,rmbCancelsAdding:!0,tzOffset:o,timeIndicatorSerifs:!1,showIntermission:!1,intermissionRange:[1,60],showPopovers:"default",canAddIntoLocation:function(t,e){return!0},beforeAddIntoLocation:function(t,e){},postRenderLocation:function(t,e,n){a.prototype.postRenderLocation.call(this,t,e,n)},postRenderEvent:function(t,e){}},h.skedTape=function(t){return h("<div/>").skedTape(h.extend({},t||{},{deferRender:!0}))}});