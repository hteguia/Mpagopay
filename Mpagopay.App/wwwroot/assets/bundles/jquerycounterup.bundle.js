!function(){"use strict";var e=0,r={};function i(t){if(!t)throw new Error("No options passed to Waypoint constructor");if(!t.element)throw new Error("No element option passed to Waypoint constructor");if(!t.handler)throw new Error("No handler option passed to Waypoint constructor");this.key="waypoint-"+e,this.options=i.Adapter.extend({},i.defaults,t),this.element=this.options.element,this.adapter=new i.Adapter(this.element),this.callback=t.handler,this.axis=this.options.horizontal?"horizontal":"vertical",this.enabled=this.options.enabled,this.triggerPoint=null,this.group=i.Group.findOrCreate({name:this.options.group,axis:this.axis}),this.context=i.Context.findOrCreateByElement(this.options.context),i.offsetAliases[this.options.offset]&&(this.options.offset=i.offsetAliases[this.options.offset]),this.group.add(this),this.context.add(this),r[this.key]=this,e+=1}i.prototype.queueTrigger=function(t){this.group.queueTrigger(this,t)},i.prototype.trigger=function(t){this.enabled&&this.callback&&this.callback.apply(this,t)},i.prototype.destroy=function(){this.context.remove(this),this.group.remove(this),delete r[this.key]},i.prototype.disable=function(){return this.enabled=!1,this},i.prototype.enable=function(){return this.context.refresh(),this.enabled=!0,this},i.prototype.next=function(){return this.group.next(this)},i.prototype.previous=function(){return this.group.previous(this)},i.invokeAll=function(t){var e,i=[];for(e in r)i.push(r[e]);for(var o=0,n=i.length;o<n;o++)i[o][t]()},i.destroyAll=function(){i.invokeAll("destroy")},i.disableAll=function(){i.invokeAll("disable")},i.enableAll=function(){for(var t in i.Context.refreshAll(),r)r[t].enabled=!0;return this},i.refreshAll=function(){i.Context.refreshAll()},i.viewportHeight=function(){return window.innerHeight||document.documentElement.clientHeight},i.viewportWidth=function(){return document.documentElement.clientWidth},i.adapters=[],i.defaults={context:window,continuous:!0,enabled:!0,group:"default",horizontal:!1,offset:0},i.offsetAliases={"bottom-in-view":function(){return this.context.innerHeight()-this.adapter.outerHeight()},"right-in-view":function(){return this.context.innerWidth()-this.adapter.outerWidth()}},window.Waypoint=i}(),function(){"use strict";function e(t){window.setTimeout(t,1e3/60)}var i=0,o={},d=window.Waypoint,t=window.onload;function n(t){this.element=t,this.Adapter=d.Adapter,this.adapter=new this.Adapter(t),this.key="waypoint-context-"+i,this.didScroll=!1,this.didResize=!1,this.oldScroll={x:this.adapter.scrollLeft(),y:this.adapter.scrollTop()},this.waypoints={vertical:{},horizontal:{}},t.waypointContextKey=this.key,o[t.waypointContextKey]=this,i+=1,d.windowContext||(d.windowContext=!0,d.windowContext=new n(window)),this.createThrottledScrollHandler(),this.createThrottledResizeHandler()}n.prototype.add=function(t){var e=t.options.horizontal?"horizontal":"vertical";this.waypoints[e][t.key]=t,this.refresh()},n.prototype.checkEmpty=function(){var t=this.Adapter.isEmptyObject(this.waypoints.horizontal),e=this.Adapter.isEmptyObject(this.waypoints.vertical),i=this.element==this.element.window;t&&e&&!i&&(this.adapter.off(".waypoints"),delete o[this.key])},n.prototype.createThrottledResizeHandler=function(){var t=this;function e(){t.handleResize(),t.didResize=!1}this.adapter.on("resize.waypoints",function(){t.didResize||(t.didResize=!0,d.requestAnimationFrame(e))})},n.prototype.createThrottledScrollHandler=function(){var t=this;function e(){t.handleScroll(),t.didScroll=!1}this.adapter.on("scroll.waypoints",function(){t.didScroll&&!d.isTouch||(t.didScroll=!0,d.requestAnimationFrame(e))})},n.prototype.handleResize=function(){d.Context.refreshAll()},n.prototype.handleScroll=function(){var t,e,i={},o={horizontal:{newScroll:this.adapter.scrollLeft(),oldScroll:this.oldScroll.x,forward:"right",backward:"left"},vertical:{newScroll:this.adapter.scrollTop(),oldScroll:this.oldScroll.y,forward:"down",backward:"up"}};for(t in o){var n,r=o[t],s=r.newScroll>r.oldScroll?r.forward:r.backward;for(n in this.waypoints[t]){var a,l,h=this.waypoints[t][n];null!==h.triggerPoint&&(a=r.oldScroll<h.triggerPoint,l=r.newScroll>=h.triggerPoint,a&&l||!a&&!l)&&(h.queueTrigger(s),i[h.group.id]=h.group)}}for(e in i)i[e].flushTriggers();this.oldScroll={x:o.horizontal.newScroll,y:o.vertical.newScroll}},n.prototype.innerHeight=function(){return this.element==this.element.window?d.viewportHeight():this.adapter.innerHeight()},n.prototype.remove=function(t){delete this.waypoints[t.axis][t.key],this.checkEmpty()},n.prototype.innerWidth=function(){return this.element==this.element.window?d.viewportWidth():this.adapter.innerWidth()},n.prototype.destroy=function(){var t,e=[];for(t in this.waypoints)for(var i in this.waypoints[t])e.push(this.waypoints[t][i]);for(var o=0,n=e.length;o<n;o++)e[o].destroy()},n.prototype.refresh=function(){var t,e,i=this.element==this.element.window,o=i?void 0:this.adapter.offset(),n={};for(e in this.handleScroll(),t={horizontal:{contextOffset:i?0:o.left,contextScroll:i?0:this.oldScroll.x,contextDimension:this.innerWidth(),oldScroll:this.oldScroll.x,forward:"right",backward:"left",offsetProp:"left"},vertical:{contextOffset:i?0:o.top,contextScroll:i?0:this.oldScroll.y,contextDimension:this.innerHeight(),oldScroll:this.oldScroll.y,forward:"down",backward:"up",offsetProp:"top"}}){var r,s=t[e];for(r in this.waypoints[e]){var a,l=this.waypoints[e][r],h=l.options.offset,u=l.triggerPoint,c=0,p=null==u;l.element!==l.element.window&&(c=l.adapter.offset()[s.offsetProp]),"function"==typeof h?h=h.apply(l):"string"==typeof h&&(h=parseFloat(h),-1<l.options.offset.indexOf("%"))&&(h=Math.ceil(s.contextDimension*h/100)),a=s.contextScroll-s.contextOffset,l.triggerPoint=Math.floor(c+a-h),c=u<s.oldScroll,a=l.triggerPoint>=s.oldScroll,h=!c&&!a,!p&&(c&&a)?(l.queueTrigger(s.backward),n[l.group.id]=l.group):(!p&&h||p&&s.oldScroll>=l.triggerPoint)&&(l.queueTrigger(s.forward),n[l.group.id]=l.group)}}return d.requestAnimationFrame(function(){for(var t in n)n[t].flushTriggers()}),this},n.findOrCreateByElement=function(t){return n.findByElement(t)||new n(t)},n.refreshAll=function(){for(var t in o)o[t].refresh()},n.findByElement=function(t){return o[t.waypointContextKey]},window.onload=function(){t&&t(),n.refreshAll()},d.requestAnimationFrame=function(t){(window.requestAnimationFrame||window.mozRequestAnimationFrame||window.webkitRequestAnimationFrame||e).call(window,t)},d.Context=n}(),function(){"use strict";function r(t,e){return t.triggerPoint-e.triggerPoint}function s(t,e){return e.triggerPoint-t.triggerPoint}var e={vertical:{},horizontal:{}},i=window.Waypoint;function o(t){this.name=t.name,this.axis=t.axis,this.id=this.name+"-"+this.axis,this.waypoints=[],this.clearTriggerQueues(),e[this.axis][this.name]=this}o.prototype.add=function(t){this.waypoints.push(t)},o.prototype.clearTriggerQueues=function(){this.triggerQueues={up:[],down:[],left:[],right:[]}},o.prototype.flushTriggers=function(){for(var t in this.triggerQueues){var e=this.triggerQueues[t];e.sort("up"===t||"left"===t?s:r);for(var i=0,o=e.length;i<o;i+=1){var n=e[i];!n.options.continuous&&i!==e.length-1||n.trigger([t])}}this.clearTriggerQueues()},o.prototype.next=function(t){this.waypoints.sort(r);t=i.Adapter.inArray(t,this.waypoints);return t===this.waypoints.length-1?null:this.waypoints[t+1]},o.prototype.previous=function(t){this.waypoints.sort(r);t=i.Adapter.inArray(t,this.waypoints);return t?this.waypoints[t-1]:null},o.prototype.queueTrigger=function(t,e){this.triggerQueues[e].push(t)},o.prototype.remove=function(t){t=i.Adapter.inArray(t,this.waypoints);-1<t&&this.waypoints.splice(t,1)},o.prototype.first=function(){return this.waypoints[0]},o.prototype.last=function(){return this.waypoints[this.waypoints.length-1]},o.findOrCreate=function(t){return e[t.axis][t.name]||new o(t)},i.Group=o}(),function(){"use strict";var i=window.jQuery,t=window.Waypoint;function o(t){this.$element=i(t)}i.each(["innerHeight","innerWidth","off","offset","on","outerHeight","outerWidth","scrollLeft","scrollTop"],function(t,e){o.prototype[e]=function(){var t=Array.prototype.slice.call(arguments);return this.$element[e].apply(this.$element,t)}}),i.each(["extend","inArray","isEmptyObject"],function(t,e){o[e]=i[e]}),t.adapters.push({name:"jquery",Adapter:o}),t.Adapter=o}(),function(){"use strict";var n=window.Waypoint;function t(o){return function(){var e=[],i=arguments[0];return o.isFunction(arguments[0])&&((i=o.extend({},arguments[1])).handler=arguments[0]),this.each(function(){var t=o.extend({},i,{element:this});"string"==typeof t.context&&(t.context=o(this).closest(t.context)[0]),e.push(new n(t))}),e}}window.jQuery&&(window.jQuery.fn.waypoint=t(window.jQuery)),window.Zepto&&(window.Zepto.fn.waypoint=t(window.Zepto))}(),function(w){"use strict";w.fn.counterUp=function(t){var d,f=w.extend({time:400,delay:10,offset:100,beginAt:0,formatter:!1,context:"window",callback:function(){}},t);return this.each(function(){var c=w(this),p={time:w(this).data("counterup-time")||f.time,delay:w(this).data("counterup-delay")||f.delay,offset:w(this).data("counterup-offset")||f.offset,beginAt:w(this).data("counterup-beginat")||f.beginAt,context:w(this).data("counterup-context")||f.context};c.waypoint(function(t){!function(){var t=[],e=p.time/p.delay,i=w(this).attr("data-num")?w(this).attr("data-num"):c.text(),o=/[0-9]+,[0-9]+/.test(i),n=((i=i.replace(/,/g,"")).split(".")[1]||[]).length,r=(p.beginAt>i&&(p.beginAt=i),/[0-9]+:[0-9]+:[0-9]+/.test(i));if(r){var s=i.split(":"),a=1;for(d=0;0<s.length;)d+=a*parseInt(s.pop(),10),a*=60}for(var l=e;l>=p.beginAt/i*e;l--){var h,u=parseFloat(i/e*l).toFixed(n);if(r&&(u=parseInt(d/e*l),u=((h=parseInt(u/3600)%24)<10?"0"+h:h)+":"+((h=parseInt(u/60)%60)<10?"0"+h:h)+":"+((h=parseInt(u%60,10))<10?"0"+h:h)),o)for(;/(\d+)(\d{3})/.test(u.toString());)u=u.toString().replace(/(\d+)(\d{3})/,"$1,$2");f.formatter&&(u=f.formatter.call(this,u)),t.unshift(u)}c.data("counterup-nums",t),c.text(p.beginAt);c.data("counterup-func",function(){c.data("counterup-nums")?(c.html(c.data("counterup-nums").shift()),c.data("counterup-nums").length?setTimeout(c.data("counterup-func"),p.delay):(c.data("counterup-nums",null),c.data("counterup-func",null),f.callback.call(this))):f.callback.call(this)}),setTimeout(c.data("counterup-func"),p.delay)}(),this.destroy()},{offset:p.offset+"%",context:p.context})})}}(jQuery);