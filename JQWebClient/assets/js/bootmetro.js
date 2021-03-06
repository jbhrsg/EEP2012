/*! bootmetro 2013-06-18 */
/*!
 * jQuery Charms Plugin
 * Original author: @aozora
 * Licensed under the MIT license
 */

(function($, window, document, undefined){

   // undefined is used here as the undefined global
   // variable in ECMAScript 3 and is mutable (i.e. it can
   // be changed by someone else). undefined isn't really
   // being passed in so we can ensure that its value is
   // truly undefined. In ES5, undefined can no longer be
   // modified.

   // window and document are passed through as local
   // variables rather than as globals, because this (slightly)
   // quickens the resolution process and can be more
   // efficiently minified (especially when both are
   // regularly referenced in your plugin).

   // Create the defaults once
   var pluginName = 'charms',
       defaults = {
          width: '320px',
          animateDuration: 600
       };

   // The actual plugin constructor

   function Charms(element, options){
      this.element = element;

      // jQuery has an extend method that merges the
      // contents of two or more objects, storing the
      // result in the first object. The first object
      // is generally empty because we don't want to alter
      // the default options for future instances of the plugin
      this.options = $.extend({ }, defaults, options);

      this._defaults = defaults;
      this._name = pluginName;

      this.init();
   }

   Charms.prototype = {
      init: function(){
         // Place initialization logic here
         // You already have access to the DOM element and
         // the options via the instance, e.g. this.element
         // and this.options
         // you can add more functions like the one below and 
         // call them like so: this.yourotherfunction(this.element, this.options).
      },

      showSection: function(sectionId, width){
         var w = this.options.width;

         if (width !== undefined){
            w = width;
         }

         var transition = $.support.transition && $(this.element).hasClass('slide');

         if (transition) {
            var ow = $(this.element).eq(0).offsetWidth; // force reflow
         }

         $(this.element).addClass('in');

         return false;
      },

      close: function(){
         $(this.element).removeClass('in');
         return false;
      },

      toggleSection: function(sectionId, width){
         if ($(this.element).hasClass('in')){
            this.close();
         }else{
            this.showSection(sectionId, width);
         }
      }//,
//
//      togglePin: function () {
//         var isPinned = $.cookie('charms_pinned');
//
//         if (isPinned == null)
//         {
//            // pin
//            $.cookie('charms_pinned', 'true');
//            $.cookie('charms_width', $(this.element).width() );
//            $("a#pin-charms").addClass("active");
//         }
//         else
//         {
//            // unpin
//            $.cookie('charms_pinned', null);
//            $.cookie('charms_width', null );
//            $("a#pin-charms").removeClass("active");
//         }
//      }
      
   };


   // A really lightweight plugin wrapper around the constructor, 
   // preventing against multiple instantiations and allowing any
   // public function (ie. a function whose name doesn't start
   // with an underscore) to be called via the jQuery plugin,
   // e.g. $(element).defaultPluginName('functionName', arg1, arg2)
   $.fn[pluginName] = function(options){
      var args = arguments;
      if (options === undefined || typeof options === 'object')
      {
         return this.each(function(){
            if (!$.data(this, 'plugin_' + pluginName))
            {
               $.data(this, 'plugin_' + pluginName, new Charms(this, options));
            }
         });
      } else if (typeof options === 'string' && options[0] !== '_' && options !== 'init')
      {
         return this.each(function(){
            var instance = $.data(this, 'plugin_' + pluginName);
            if (instance instanceof Charms && typeof instance[options] === 'function')
            {
               instance[options].apply(instance, Array.prototype.slice.call(args, 1));
            }
         });
      }
   };

})(jQuery, window, document);


(function ($)
{
   $("#charms").charms();
   
   $('.close-charms').click(function(e){
      e.preventDefault();
      $("#charms").charms('close');
   });

   /*$("a#pin-charms").click(function () {
      $(this)
      $("#charms").charms('togglePin');
   });*/


})(jQuery);

/* Load this script using conditional IE comments if you need to support IE 7 and IE 6. */

window.onload = function() {
	function addIcon(el, entity) {
		var html = el.innerHTML;
		el.innerHTML = '<span style="font-family: \'icomoon\'">' + entity + '</span>' + html;
	}
	var icons = {
			'icon-battery-low' : '&#xe000;',
			'icon-battery' : '&#xe001;',
			'icon-battery-full' : '&#xe002;',
			'icon-battery-charging' : '&#xe003;',
			'icon-plus' : '&#xe004;',
			'icon-cross' : '&#xe005;',
			'icon-arrow-right' : '&#xe006;',
			'icon-arrow-left' : '&#xe007;',
			'icon-pencil' : '&#xe008;',
			'icon-search' : '&#xe009;',
			'icon-grid' : '&#xe00a;',
			'icon-list' : '&#xe00b;',
			'icon-star' : '&#xe00c;',
			'icon-heart' : '&#xe00d;',
			'icon-back' : '&#xe00e;',
			'icon-forward' : '&#xe00f;',
			'icon-map-marker' : '&#xe010;',
			'icon-phone' : '&#xe011;',
			'icon-home' : '&#xe012;',
			'icon-camera' : '&#xe013;',
			'icon-arrow-left-2' : '&#xe014;',
			'icon-arrow-right-2' : '&#xe015;',
			'icon-arrow-up' : '&#xe016;',
			'icon-arrow-down' : '&#xe017;',
			'icon-refresh' : '&#xe018;',
			'icon-refresh-2' : '&#xe019;',
			'icon-escape' : '&#xe01a;',
			'icon-repeat' : '&#xe01b;',
			'icon-loop' : '&#xe01c;',
			'icon-shuffle' : '&#xe01d;',
			'icon-feed' : '&#xe01e;',
			'icon-cog' : '&#xe01f;',
			'icon-wrench' : '&#xe020;',
			'icon-bars' : '&#xe021;',
			'icon-chart' : '&#xe022;',
			'icon-stats' : '&#xe023;',
			'icon-eye' : '&#xe024;',
			'icon-zoom-out' : '&#xe025;',
			'icon-zoom-in' : '&#xe026;',
			'icon-export' : '&#xe027;',
			'icon-user' : '&#xe028;',
			'icon-users' : '&#xe029;',
			'icon-microphone' : '&#xe02a;',
			'icon-mail' : '&#xe02b;',
			'icon-comment' : '&#xe02c;',
			'icon-trashcan' : '&#xe02d;',
			'icon-delete' : '&#xe02e;',
			'icon-infinity' : '&#xe02f;',
			'icon-key' : '&#xe030;',
			'icon-globe' : '&#xe031;',
			'icon-thumbs-up' : '&#xe032;',
			'icon-thumbs-down' : '&#xe033;',
			'icon-tag' : '&#xe034;',
			'icon-views' : '&#xe035;',
			'icon-warning' : '&#xe036;',
			'icon-beta' : '&#xe037;',
			'icon-unlocked' : '&#xe038;',
			'icon-locked' : '&#xe039;',
			'icon-eject' : '&#xe03a;',
			'icon-move' : '&#xe03b;',
			'icon-expand' : '&#xe03c;',
			'icon-cancel' : '&#xe03d;',
			'icon-electricity' : '&#xe03e;',
			'icon-compass' : '&#xe03f;',
			'icon-location' : '&#xe040;',
			'icon-directions' : '&#xe041;',
			'icon-pin' : '&#xe042;',
			'icon-mute' : '&#xe043;',
			'icon-volume' : '&#xe044;',
			'icon-globe-2' : '&#xe045;',
			'icon-pencil-2' : '&#xe046;',
			'icon-minus' : '&#xe047;',
			'icon-equals' : '&#xe048;',
			'icon-list-2' : '&#xe049;',
			'icon-flag' : '&#xe04a;',
			'icon-info' : '&#xe04b;',
			'icon-question' : '&#xe04c;',
			'icon-chat' : '&#xe04d;',
			'icon-clock' : '&#xe04e;',
			'icon-calendar' : '&#xe04f;',
			'icon-sun' : '&#xe050;',
			'icon-contrast' : '&#xe051;',
			'icon-mobile' : '&#xe052;',
			'icon-download' : '&#xe053;',
			'icon-puzzle' : '&#xe054;',
			'icon-music' : '&#xe055;',
			'icon-scissors' : '&#xe056;',
			'icon-bookmark' : '&#xe057;',
			'icon-anchor' : '&#xe058;',
			'icon-checkmark' : '&#xe059;',
			'icon-phone-2' : '&#xe05a;',
			'icon-mobile-2' : '&#xe05b;',
			'icon-mouse' : '&#xe05c;',
			'icon-directions-2' : '&#xe05d;',
			'icon-mail-2' : '&#xe05e;',
			'icon-paperplane' : '&#xe05f;',
			'icon-pencil-3' : '&#xe060;',
			'icon-feather' : '&#xe061;',
			'icon-paperclip' : '&#xe062;',
			'icon-drawer' : '&#xe063;',
			'icon-reply' : '&#xe064;',
			'icon-reply-all' : '&#xe065;',
			'icon-forward-2' : '&#xe066;',
			'icon-user-2' : '&#xe067;',
			'icon-users-2' : '&#xe068;',
			'icon-user-add' : '&#xe069;',
			'icon-vcard' : '&#xe06a;',
			'icon-export-2' : '&#xe06b;',
			'icon-location-2' : '&#xe06c;',
			'icon-map' : '&#xe06d;',
			'icon-compass-2' : '&#xe06e;',
			'icon-location-3' : '&#xe06f;',
			'icon-target' : '&#xe070;',
			'icon-share' : '&#xe071;',
			'icon-sharable' : '&#xe072;',
			'icon-heart-2' : '&#xe073;',
			'icon-heart-3' : '&#xe074;',
			'icon-star-2' : '&#xe075;',
			'icon-star-3' : '&#xe076;',
			'icon-thumbs-up-2' : '&#xe077;',
			'icon-thumbs-down-2' : '&#xe078;',
			'icon-chat-2' : '&#xe079;',
			'icon-comment-2' : '&#xe07a;',
			'icon-quote' : '&#xe07b;',
			'icon-house' : '&#xe07c;',
			'icon-popup' : '&#xe07d;',
			'icon-search-2' : '&#xe07e;',
			'icon-flashlight' : '&#xe07f;',
			'icon-printer' : '&#xe080;',
			'icon-bell' : '&#xe081;',
			'icon-link' : '&#xe082;',
			'icon-flag-2' : '&#xe083;',
			'icon-cog-2' : '&#xe084;',
			'icon-tools' : '&#xe085;',
			'icon-trophy' : '&#xe086;',
			'icon-tag-2' : '&#xe087;',
			'icon-camera-2' : '&#xe088;',
			'icon-megaphone' : '&#xe089;',
			'icon-moon' : '&#xe08a;',
			'icon-palette' : '&#xe08b;',
			'icon-leaf' : '&#xe08c;',
			'icon-music-2' : '&#xe08d;',
			'icon-music-3' : '&#xe08e;',
			'icon-new' : '&#xe08f;',
			'icon-graduation' : '&#xe090;',
			'icon-book' : '&#xe091;',
			'icon-newspaper' : '&#xe092;',
			'icon-bag' : '&#xe093;',
			'icon-airplane' : '&#xe094;',
			'icon-lifebuoy' : '&#xe095;',
			'icon-eye-2' : '&#xe096;',
			'icon-clock-2' : '&#xe097;',
			'icon-microphone-2' : '&#xe098;',
			'icon-calendar-2' : '&#xe099;',
			'icon-bolt' : '&#xe09a;',
			'icon-thunder' : '&#xe09b;',
			'icon-droplet' : '&#xe09c;',
			'icon-cd' : '&#xe09d;',
			'icon-briefcase' : '&#xe09e;',
			'icon-air' : '&#xe09f;',
			'icon-hourglass' : '&#xe0a0;',
			'icon-gauge' : '&#xe0a1;',
			'icon-language' : '&#xe0a2;',
			'icon-network' : '&#xe0a3;',
			'icon-key-2' : '&#xe0a4;',
			'icon-battery-2' : '&#xe0a5;',
			'icon-bucket' : '&#xe0a6;',
			'icon-magnet' : '&#xe0a7;',
			'icon-drive' : '&#xe0a8;',
			'icon-cup' : '&#xe0a9;',
			'icon-rocket' : '&#xe0aa;',
			'icon-brush' : '&#xe0ab;',
			'icon-suitcase' : '&#xe0ac;',
			'icon-cone' : '&#xe0ad;',
			'icon-earth' : '&#xe0ae;',
			'icon-keyboard' : '&#xe0af;',
			'icon-browser' : '&#xe0b0;',
			'icon-publish' : '&#xe0b1;',
			'icon-progress-3' : '&#xe0b2;',
			'icon-progress-2' : '&#xe0b3;',
			'icon-brogress-1' : '&#xe0b4;',
			'icon-progress-0' : '&#xe0b5;',
			'icon-sun-2' : '&#xe0b6;',
			'icon-sun-3' : '&#xe0b7;',
			'icon-adjust' : '&#xe0b8;',
			'icon-code' : '&#xe0b9;',
			'icon-screen' : '&#xe0ba;',
			'icon-infinity-2' : '&#xe0bb;',
			'icon-light-bulb' : '&#xe0bc;',
			'icon-credit-card' : '&#xe0bd;',
			'icon-database' : '&#xe0be;',
			'icon-voicemail' : '&#xe0bf;',
			'icon-clipboard' : '&#xe0c0;',
			'icon-cart' : '&#xe0c1;',
			'icon-box' : '&#xe0c2;',
			'icon-ticket' : '&#xe0c3;',
			'icon-rss' : '&#xe0c4;',
			'icon-signal' : '&#xe0c5;',
			'icon-thermometer' : '&#xe0c6;',
			'icon-droplets' : '&#xe0c7;',
			'icon-untitled' : '&#xe0c8;',
			'icon-statistics' : '&#xe0c9;',
			'icon-pie' : '&#xe0ca;',
			'icon-bars-2' : '&#xe0cb;',
			'icon-graph' : '&#xe0cc;',
			'icon-lock' : '&#xe0cd;',
			'icon-lock-open' : '&#xe0ce;',
			'icon-logout' : '&#xe0cf;',
			'icon-login' : '&#xe0d0;',
			'icon-checkmark-2' : '&#xe0d1;',
			'icon-cross-2' : '&#xe0d2;',
			'icon-minus-2' : '&#xe0d3;',
			'icon-plus-2' : '&#xe0d4;',
			'icon-cross-3' : '&#xe0d5;',
			'icon-minus-3' : '&#xe0d6;',
			'icon-plus-3' : '&#xe0d7;',
			'icon-cross-4' : '&#xe0d8;',
			'icon-minus-4' : '&#xe0d9;',
			'icon-plus-4' : '&#xe0da;',
			'icon-erase' : '&#xe0db;',
			'icon-blocked' : '&#xe0dc;',
			'icon-info-2' : '&#xe0dd;',
			'icon-info-3' : '&#xe0de;',
			'icon-question-2' : '&#xe0df;',
			'icon-help' : '&#xe0e0;',
			'icon-warning-2' : '&#xe0e1;',
			'icon-cycle' : '&#xe0e2;',
			'icon-cw' : '&#xe0e3;',
			'icon-ccw' : '&#xe0e4;',
			'icon-shuffle-2' : '&#xe0e5;',
			'icon-arrow' : '&#xe0e6;',
			'icon-arrow-2' : '&#xe0e7;',
			'icon-retweet' : '&#xe0e8;',
			'icon-loop-2' : '&#xe0e9;',
			'icon-history' : '&#xe0ea;',
			'icon-back-2' : '&#xe0eb;',
			'icon-switch' : '&#xe0ec;',
			'icon-list-3' : '&#xe0ed;',
			'icon-add-to-list' : '&#xe0ee;',
			'icon-layout' : '&#xe0ef;',
			'icon-list-4' : '&#xe0f0;',
			'icon-text' : '&#xe0f1;',
			'icon-text-2' : '&#xe0f2;',
			'icon-document' : '&#xe0f3;',
			'icon-docs' : '&#xe0f4;',
			'icon-landscape' : '&#xe0f5;',
			'icon-pictures' : '&#xe0f6;',
			'icon-video' : '&#xe0f7;',
			'icon-music-4' : '&#xe0f8;',
			'icon-folder' : '&#xe0f9;',
			'icon-archive' : '&#xe0fa;',
			'icon-trash' : '&#xe0fb;',
			'icon-upload' : '&#xe0fc;',
			'icon-download-2' : '&#xe0fd;',
			'icon-disk' : '&#xe0fe;',
			'icon-install' : '&#xe0ff;',
			'icon-cloud' : '&#xe100;',
			'icon-upload-2' : '&#xe101;',
			'icon-bookmark-2' : '&#xe102;',
			'icon-bookmarks' : '&#xe103;',
			'icon-book-2' : '&#xe104;',
			'icon-play' : '&#xe105;',
			'icon-pause' : '&#xe106;',
			'icon-record' : '&#xe107;',
			'icon-stop' : '&#xe108;',
			'icon-next' : '&#xe109;',
			'icon-previous' : '&#xe10a;',
			'icon-first' : '&#xe10b;',
			'icon-last' : '&#xe10c;',
			'icon-resize-enlarge' : '&#xe10d;',
			'icon-resize-shrink' : '&#xe10e;',
			'icon-volume-2' : '&#xe10f;',
			'icon-sound' : '&#xe110;',
			'icon-mute-2' : '&#xe111;',
			'icon-flow-cascade' : '&#xe112;',
			'icon-flow-branch' : '&#xe113;',
			'icon-flow-tree' : '&#xe114;',
			'icon-flow-line' : '&#xe115;',
			'icon-flow-parallel' : '&#xe116;',
			'icon-arrow-left-3' : '&#xe117;',
			'icon-arrow-down-2' : '&#xe118;',
			'icon-arrow-up--upload' : '&#xe119;',
			'icon-arrow-right-3' : '&#xe11a;',
			'icon-arrow-left-4' : '&#xe11b;',
			'icon-arrow-down-3' : '&#xe11c;',
			'icon-arrow-up-2' : '&#xe11d;',
			'icon-arrow-right-4' : '&#xe11e;',
			'icon-arrow-left-5' : '&#xe11f;',
			'icon-arrow-down-4' : '&#xe120;',
			'icon-arrow-up-3' : '&#xe121;',
			'icon-arrow-right-5' : '&#xe122;',
			'icon-arrow-left-6' : '&#xe123;',
			'icon-arrow-down-5' : '&#xe124;',
			'icon-arrow-up-4' : '&#xe125;',
			'icon-arrow-right-6' : '&#xe126;',
			'icon-arrow-left-7' : '&#xe127;',
			'icon-arrow-down-6' : '&#xe128;',
			'icon-arrow-up-5' : '&#xe129;',
			'icon-arrow-right-7' : '&#xe12a;',
			'icon-arrow-left-8' : '&#xe12b;',
			'icon-arrow-down-7' : '&#xe12c;',
			'icon-arrow-up-6' : '&#xe12d;',
			'icon-arrow-right-8' : '&#xe12e;',
			'icon-arrow-left-9' : '&#xe12f;',
			'icon-arrow-down-8' : '&#xe130;',
			'icon-arrow-up-7' : '&#xe131;',
			'icon-untitled-2' : '&#xe132;',
			'icon-arrow-left-10' : '&#xe133;',
			'icon-arrow-down-9' : '&#xe134;',
			'icon-arrow-up-8' : '&#xe135;',
			'icon-arrow-right-9' : '&#xe136;',
			'icon-menu' : '&#xe137;',
			'icon-ellipsis' : '&#xe138;',
			'icon-dots' : '&#xe139;',
			'icon-dot' : '&#xe13a;',
			'icon-cc' : '&#xe13b;',
			'icon-cc-by' : '&#xe13c;',
			'icon-cc-nc' : '&#xe13d;',
			'icon-cc-nc-eu' : '&#xe13e;',
			'icon-cc-nc-jp' : '&#xe13f;',
			'icon-cc-sa' : '&#xe140;',
			'icon-cc-nd' : '&#xe141;',
			'icon-cc-pd' : '&#xe142;',
			'icon-cc-zero' : '&#xe143;',
			'icon-cc-share' : '&#xe144;',
			'icon-cc-share-2' : '&#xe145;',
			'icon-daniel-bruce' : '&#xe146;',
			'icon-daniel-bruce-2' : '&#xe147;',
			'icon-github' : '&#xe148;',
			'icon-github-2' : '&#xe149;',
			'icon-flickr' : '&#xe14a;',
			'icon-flickr-2' : '&#xe14b;',
			'icon-vimeo' : '&#xe14c;',
			'icon-vimeo-2' : '&#xe14d;',
			'icon-twitter' : '&#xe14e;',
			'icon-twitter-2' : '&#xe14f;',
			'icon-facebook' : '&#xe150;',
			'icon-facebook-2' : '&#xe151;',
			'icon-facebook-3' : '&#xe152;',
			'icon-googleplus' : '&#xe153;',
			'icon-googleplus-2' : '&#xe154;',
			'icon-pinterest' : '&#xe155;',
			'icon-pinterest-2' : '&#xe156;',
			'icon-tumblr' : '&#xe157;',
			'icon-tumblr-2' : '&#xe158;',
			'icon-linkedin' : '&#xe159;',
			'icon-linkedin-2' : '&#xe15a;',
			'icon-dribbble' : '&#xe15b;',
			'icon-dribbble-2' : '&#xe15c;',
			'icon-stumbleupon' : '&#xe15d;',
			'icon-stumbleupon-2' : '&#xe15e;',
			'icon-lastfm' : '&#xe15f;',
			'icon-lastfm-2' : '&#xe160;',
			'icon-rdio' : '&#xe161;',
			'icon-rdio-2' : '&#xe162;',
			'icon-spotify' : '&#xe163;',
			'icon-spotify-2' : '&#xe164;',
			'icon-qq' : '&#xe165;',
			'icon-instagram' : '&#xe166;',
			'icon-dropbox' : '&#xe167;',
			'icon-evernote' : '&#xe168;',
			'icon-flattr' : '&#xe169;',
			'icon-skype' : '&#xe16a;',
			'icon-skype-2' : '&#xe16b;',
			'icon-renren' : '&#xe16c;',
			'icon-sina-weibo' : '&#xe16d;',
			'icon-paypal' : '&#xe16e;',
			'icon-picasa' : '&#xe16f;',
			'icon-soundcloud' : '&#xe170;',
			'icon-mixi' : '&#xe171;',
			'icon-behance' : '&#xe172;',
			'icon-circles' : '&#xe173;',
			'icon-vk' : '&#xe174;',
			'icon-smashing' : '&#xe175;',
			'icon-home-2' : '&#xe176;',
			'icon-home-3' : '&#xe177;',
			'icon-home-4' : '&#xe178;',
			'icon-office' : '&#xe179;',
			'icon-newspaper-2' : '&#xe17a;',
			'icon-pencil-4' : '&#xe17b;',
			'icon-pencil-5' : '&#xe17c;',
			'icon-quill' : '&#xe17d;',
			'icon-pen' : '&#xe17e;',
			'icon-blog' : '&#xe17f;',
			'icon-droplet-2' : '&#xe180;',
			'icon-paint-format' : '&#xe181;',
			'icon-image' : '&#xe182;',
			'icon-image-2' : '&#xe183;',
			'icon-images' : '&#xe184;',
			'icon-camera-3' : '&#xe185;',
			'icon-music-5' : '&#xe186;',
			'icon-headphones' : '&#xe187;',
			'icon-play-2' : '&#xe188;',
			'icon-film' : '&#xe189;',
			'icon-camera-4' : '&#xe18a;',
			'icon-dice' : '&#xe18b;',
			'icon-pacman' : '&#xe18c;',
			'icon-spades' : '&#xe18d;',
			'icon-clubs' : '&#xe18e;',
			'icon-diamonds' : '&#xe18f;',
			'icon-pawn' : '&#xe190;',
			'icon-bullhorn' : '&#xe191;',
			'icon-connection' : '&#xe192;',
			'icon-podcast' : '&#xe193;',
			'icon-feed-2' : '&#xe194;',
			'icon-book-3' : '&#xe195;',
			'icon-books' : '&#xe196;',
			'icon-library' : '&#xe197;',
			'icon-file' : '&#xe198;',
			'icon-profile' : '&#xe199;',
			'icon-file-2' : '&#xe19a;',
			'icon-file-3' : '&#xe19b;',
			'icon-file-4' : '&#xe19c;',
			'icon-copy' : '&#xe19d;',
			'icon-copy-2' : '&#xe19e;',
			'icon-copy-3' : '&#xe19f;',
			'icon-paste' : '&#xe1a0;',
			'icon-paste-2' : '&#xe1a1;',
			'icon-paste-3' : '&#xe1a2;',
			'icon-stack' : '&#xe1a3;',
			'icon-folder-2' : '&#xe1a4;',
			'icon-folder-open' : '&#xe1a5;',
			'icon-tag-3' : '&#xe1a6;',
			'icon-tags' : '&#xe1a7;',
			'icon-barcode' : '&#xe1a8;',
			'icon-qrcode' : '&#xe1a9;',
			'icon-ticket-2' : '&#xe1aa;',
			'icon-cart-2' : '&#xe1ab;',
			'icon-cart-3' : '&#xe1ac;',
			'icon-cart-4' : '&#xe1ad;',
			'icon-coin' : '&#xe1ae;',
			'icon-credit' : '&#xe1af;',
			'icon-calculate' : '&#xe1b0;',
			'icon-support' : '&#xe1b1;',
			'icon-phone-3' : '&#xe1b2;',
			'icon-phone-hang-up' : '&#xe1b3;',
			'icon-address-book' : '&#xe1b4;',
			'icon-notebook' : '&#xe1b5;',
			'icon-envelop' : '&#xe1b6;',
			'icon-pushpin' : '&#xe1b7;',
			'icon-location-4' : '&#xe1b8;',
			'icon-location-5' : '&#xe1b9;',
			'icon-compass-3' : '&#xe1ba;',
			'icon-map-2' : '&#xe1bb;',
			'icon-map-3' : '&#xe1bc;',
			'icon-history-2' : '&#xe1bd;',
			'icon-clock-3' : '&#xe1be;',
			'icon-clock-4' : '&#xe1bf;',
			'icon-alarm' : '&#xe1c0;',
			'icon-alarm-2' : '&#xe1c1;',
			'icon-bell-2' : '&#xe1c2;',
			'icon-stopwatch' : '&#xe1c3;',
			'icon-calendar-3' : '&#xe1c4;',
			'icon-calendar-4' : '&#xe1c5;',
			'icon-print' : '&#xe1c6;',
			'icon-keyboard-2' : '&#xe1c7;',
			'icon-screen-2' : '&#xe1c8;',
			'icon-laptop' : '&#xe1c9;',
			'icon-mobile-3' : '&#xe1ca;',
			'icon-mobile-4' : '&#xe1cb;',
			'icon-tablet' : '&#xe1cc;',
			'icon-tv' : '&#xe1cd;',
			'icon-cabinet' : '&#xe1ce;',
			'icon-drawer-2' : '&#xe1cf;',
			'icon-drawer-3' : '&#xe1d0;',
			'icon-drawer-4' : '&#xe1d1;',
			'icon-box-add' : '&#xe1d2;',
			'icon-box-remove' : '&#xe1d3;',
			'icon-download-3' : '&#xe1d4;',
			'icon-upload-3' : '&#xe1d5;',
			'icon-disk-2' : '&#xe1d6;',
			'icon-storage' : '&#xe1d7;',
			'icon-undo' : '&#xe1d8;',
			'icon-redo' : '&#xe1d9;',
			'icon-flip' : '&#xe1da;',
			'icon-flip-2' : '&#xe1db;',
			'icon-undo-2' : '&#xe1dc;',
			'icon-redo-2' : '&#xe1dd;',
			'icon-forward-3' : '&#xe1de;',
			'icon-reply-2' : '&#xe1df;',
			'icon-bubble' : '&#xe1e0;',
			'icon-bubbles' : '&#xe1e1;',
			'icon-bubbles-2' : '&#xe1e2;',
			'icon-bubble-2' : '&#xe1e3;',
			'icon-bubbles-3' : '&#xe1e4;',
			'icon-bubbles-4' : '&#xe1e5;',
			'icon-user-3' : '&#xe1e6;',
			'icon-users-3' : '&#xe1e7;',
			'icon-user-4' : '&#xe1e8;',
			'icon-users-4' : '&#xe1e9;',
			'icon-user-5' : '&#xe1ea;',
			'icon-user-6' : '&#xe1eb;',
			'icon-quotes-left' : '&#xe1ec;',
			'icon-busy' : '&#xe1ed;',
			'icon-spinner' : '&#xe1ee;',
			'icon-spinner-2' : '&#xe1ef;',
			'icon-spinner-3' : '&#xe1f0;',
			'icon-spinner-4' : '&#xe1f1;',
			'icon-spinner-5' : '&#xe1f2;',
			'icon-spinner-6' : '&#xe1f3;',
			'icon-binoculars' : '&#xe1f4;',
			'icon-search-3' : '&#xe1f5;',
			'icon-zoom-in-2' : '&#xe1f6;',
			'icon-zoom-out-2' : '&#xe1f7;',
			'icon-expand-2' : '&#xe1f8;',
			'icon-contract' : '&#xe1f9;',
			'icon-expand-3' : '&#xe1fa;',
			'icon-contract-2' : '&#xe1fb;',
			'icon-key-3' : '&#xe1fc;',
			'icon-key-4' : '&#xe1fd;',
			'icon-lock-2' : '&#xe1fe;',
			'icon-lock-3' : '&#xe1ff;',
			'icon-unlocked-2' : '&#xe200;',
			'icon-wrench-2' : '&#xe201;',
			'icon-settings' : '&#xe202;',
			'icon-equalizer' : '&#xe203;',
			'icon-cog-3' : '&#xe204;',
			'icon-cogs' : '&#xe205;',
			'icon-cog-4' : '&#xe206;',
			'icon-hammer' : '&#xe207;',
			'icon-wand' : '&#xe208;',
			'icon-aid' : '&#xe209;',
			'icon-bug' : '&#xe20a;',
			'icon-pie-2' : '&#xe20b;',
			'icon-stats-2' : '&#xe20c;',
			'icon-bars-3' : '&#xe20d;',
			'icon-bars-4' : '&#xe20e;',
			'icon-gift' : '&#xe20f;',
			'icon-trophy-2' : '&#xe210;',
			'icon-glass' : '&#xe211;',
			'icon-mug' : '&#xe212;',
			'icon-food' : '&#xe213;',
			'icon-leaf-2' : '&#xe214;',
			'icon-rocket-2' : '&#xe215;',
			'icon-meter' : '&#xe216;',
			'icon-meter2' : '&#xe217;',
			'icon-dashboard' : '&#xe218;',
			'icon-hammer-2' : '&#xe219;',
			'icon-fire' : '&#xe21a;',
			'icon-lab' : '&#xe21b;',
			'icon-magnet-2' : '&#xe21c;',
			'icon-remove' : '&#xe21d;',
			'icon-remove-2' : '&#xe21e;',
			'icon-briefcase-2' : '&#xe21f;',
			'icon-airplane-2' : '&#xe220;',
			'icon-truck' : '&#xe221;',
			'icon-road' : '&#xe222;',
			'icon-accessibility' : '&#xe223;',
			'icon-target-2' : '&#xe224;',
			'icon-shield' : '&#xe225;',
			'icon-lightning' : '&#xe226;',
			'icon-switch-2' : '&#xe227;',
			'icon-power-cord' : '&#xe228;',
			'icon-signup' : '&#xe229;',
			'icon-list-5' : '&#xe22a;',
			'icon-list-6' : '&#xe22b;',
			'icon-numbered-list' : '&#xe22c;',
			'icon-menu-2' : '&#xe22d;',
			'icon-menu-3' : '&#xe22e;',
			'icon-tree' : '&#xe22f;',
			'icon-cloud-2' : '&#xe230;',
			'icon-cloud-download' : '&#xe231;',
			'icon-cloud-upload' : '&#xe232;',
			'icon-download-4' : '&#xe233;',
			'icon-upload-4' : '&#xe234;',
			'icon-download-5' : '&#xe235;',
			'icon-upload-5' : '&#xe236;',
			'icon-globe-3' : '&#xe237;',
			'icon-earth-2' : '&#xe238;',
			'icon-link-2' : '&#xe239;',
			'icon-flag-3' : '&#xe23a;',
			'icon-attachment' : '&#xe23b;',
			'icon-eye-3' : '&#xe23c;',
			'icon-eye-blocked' : '&#xe23d;',
			'icon-eye-4' : '&#xe23e;',
			'icon-bookmark-3' : '&#xe23f;',
			'icon-bookmarks-2' : '&#xe240;',
			'icon-brightness-medium' : '&#xe241;',
			'icon-brightness-contrast' : '&#xe242;',
			'icon-contrast-2' : '&#xe243;',
			'icon-star-4' : '&#xe244;',
			'icon-star-5' : '&#xe245;',
			'icon-star-6' : '&#xe246;',
			'icon-heart-4' : '&#xe247;',
			'icon-heart-5' : '&#xe248;',
			'icon-heart-broken' : '&#xe249;',
			'icon-thumbs-up-3' : '&#xe24a;',
			'icon-thumbs-up-4' : '&#xe24b;',
			'icon-happy' : '&#xe24c;',
			'icon-happy-2' : '&#xe24d;',
			'icon-smiley' : '&#xe24e;',
			'icon-smiley-2' : '&#xe24f;',
			'icon-tongue' : '&#xe250;',
			'icon-tongue-2' : '&#xe251;',
			'icon-sad' : '&#xe252;',
			'icon-sad-2' : '&#xe253;',
			'icon-wink' : '&#xe254;',
			'icon-wink-2' : '&#xe255;',
			'icon-grin' : '&#xe256;',
			'icon-grin-2' : '&#xe257;',
			'icon-cool' : '&#xe258;',
			'icon-cool-2' : '&#xe259;',
			'icon-angry' : '&#xe25a;',
			'icon-angry-2' : '&#xe25b;',
			'icon-evil' : '&#xe25c;',
			'icon-evil-2' : '&#xe25d;',
			'icon-shocked' : '&#xe25e;',
			'icon-shocked-2' : '&#xe25f;',
			'icon-confused' : '&#xe260;',
			'icon-confused-2' : '&#xe261;',
			'icon-neutral' : '&#xe262;',
			'icon-neutral-2' : '&#xe263;',
			'icon-wondering' : '&#xe264;',
			'icon-wondering-2' : '&#xe265;',
			'icon-point-up' : '&#xe266;',
			'icon-point-right' : '&#xe267;',
			'icon-point-down' : '&#xe268;',
			'icon-point-left' : '&#xe269;',
			'icon-warning-3' : '&#xe26a;',
			'icon-notification' : '&#xe26b;',
			'icon-question-3' : '&#xe26c;',
			'icon-info-4' : '&#xe26d;',
			'icon-info-5' : '&#xe26e;',
			'icon-blocked-2' : '&#xe26f;',
			'icon-cancel-circle' : '&#xe270;',
			'icon-checkmark-circle' : '&#xe271;',
			'icon-spam' : '&#xe272;',
			'icon-close' : '&#xe273;',
			'icon-checkmark-3' : '&#xe274;',
			'icon-checkmark-4' : '&#xe275;',
			'icon-spell-check' : '&#xe276;',
			'icon-minus-5' : '&#xe277;',
			'icon-plus-5' : '&#xe278;',
			'icon-enter' : '&#xe279;',
			'icon-exit' : '&#xe27a;',
			'icon-play-3' : '&#xe27b;',
			'icon-pause-2' : '&#xe27c;',
			'icon-stop-2' : '&#xe27d;',
			'icon-backward' : '&#xe27e;',
			'icon-forward-4' : '&#xe27f;',
			'icon-play-4' : '&#xe280;',
			'icon-pause-3' : '&#xe281;',
			'icon-stop-3' : '&#xe282;',
			'icon-backward-2' : '&#xe283;',
			'icon-forward-5' : '&#xe284;',
			'icon-first-2' : '&#xe285;',
			'icon-last-2' : '&#xe286;',
			'icon-previous-2' : '&#xe287;',
			'icon-next-2' : '&#xe288;',
			'icon-eject-2' : '&#xe289;',
			'icon-volume-high' : '&#xe28a;',
			'icon-volume-medium' : '&#xe28b;',
			'icon-volume-low' : '&#xe28c;',
			'icon-volume-mute' : '&#xe28d;',
			'icon-volume-mute-2' : '&#xe28e;',
			'icon-volume-increase' : '&#xe28f;',
			'icon-volume-decrease' : '&#xe290;',
			'icon-loop-3' : '&#xe291;',
			'icon-loop-4' : '&#xe292;',
			'icon-loop-5' : '&#xe293;',
			'icon-shuffle-3' : '&#xe294;',
			'icon-arrow-up-left' : '&#xe295;',
			'icon-arrow-up-9' : '&#xe296;',
			'icon-arrow-up-right' : '&#xe297;',
			'icon-arrow-right-10' : '&#xe298;',
			'icon-arrow-down-right' : '&#xe299;',
			'icon-arrow-down-10' : '&#xe29a;',
			'icon-arrow-down-left' : '&#xe29b;',
			'icon-arrow-left-11' : '&#xe29c;',
			'icon-arrow-up-left-2' : '&#xe29d;',
			'icon-arrow-up-10' : '&#xe29e;',
			'icon-arrow-up-right-2' : '&#xe29f;',
			'icon-arrow-right-11' : '&#xe2a0;',
			'icon-arrow-down-right-2' : '&#xe2a1;',
			'icon-arrow-down-11' : '&#xe2a2;',
			'icon-arrow-down-left-2' : '&#xe2a3;',
			'icon-arrow-left-12' : '&#xe2a4;',
			'icon-arrow-up-left-3' : '&#xe2a5;',
			'icon-arrow-up-11' : '&#xe2a6;',
			'icon-arrow-up-right-3' : '&#xe2a7;',
			'icon-arrow-right-12' : '&#xe2a8;',
			'icon-arrow-down-right-3' : '&#xe2a9;',
			'icon-arrow-down-12' : '&#xe2aa;',
			'icon-arrow-down-left-3' : '&#xe2ab;',
			'icon-arrow-left-13' : '&#xe2ac;',
			'icon-tab' : '&#xe2ad;',
			'icon-checkbox-checked' : '&#xe2ae;',
			'icon-checkbox-unchecked' : '&#xe2af;',
			'icon-checkbox-partial' : '&#xe2b0;',
			'icon-radio-checked' : '&#xe2b1;',
			'icon-radio-unchecked' : '&#xe2b2;',
			'icon-crop' : '&#xe2b3;',
			'icon-scissors-2' : '&#xe2b4;',
			'icon-filter' : '&#xe2b5;',
			'icon-filter-2' : '&#xe2b6;',
			'icon-font' : '&#xe2b7;',
			'icon-text-height' : '&#xe2b8;',
			'icon-text-width' : '&#xe2b9;',
			'icon-bold' : '&#xe2ba;',
			'icon-underline' : '&#xe2bb;',
			'icon-italic' : '&#xe2bc;',
			'icon-strikethrough' : '&#xe2bd;',
			'icon-omega' : '&#xe2be;',
			'icon-sigma' : '&#xe2bf;',
			'icon-table' : '&#xe2c0;',
			'icon-table-2' : '&#xe2c1;',
			'icon-insert-template' : '&#xe2c2;',
			'icon-pilcrow' : '&#xe2c3;',
			'icon-left-to-right' : '&#xe2c4;',
			'icon-right-to-left' : '&#xe2c5;',
			'icon-paragraph-left' : '&#xe2c6;',
			'icon-paragraph-center' : '&#xe2c7;',
			'icon-paragraph-right' : '&#xe2c8;',
			'icon-paragraph-justify' : '&#xe2c9;',
			'icon-paragraph-left-2' : '&#xe2ca;',
			'icon-paragraph-center-2' : '&#xe2cb;',
			'icon-paragraph-right-2' : '&#xe2cc;',
			'icon-paragraph-justify-2' : '&#xe2cd;',
			'icon-indent-increase' : '&#xe2ce;',
			'icon-indent-decrease' : '&#xe2cf;',
			'icon-new-tab' : '&#xe2d0;',
			'icon-embed' : '&#xe2d1;',
			'icon-code-2' : '&#xe2d2;',
			'icon-console' : '&#xe2d3;',
			'icon-share-2' : '&#xe2d4;',
			'icon-mail-3' : '&#xe2d5;',
			'icon-mail-4' : '&#xe2d6;',
			'icon-mail-5' : '&#xe2d7;',
			'icon-mail-6' : '&#xe2d8;',
			'icon-google' : '&#xe2d9;',
			'icon-google-plus' : '&#xe2da;',
			'icon-google-plus-2' : '&#xe2db;',
			'icon-google-plus-3' : '&#xe2dc;',
			'icon-google-plus-4' : '&#xe2dd;',
			'icon-google-drive' : '&#xe2de;',
			'icon-facebook-4' : '&#xe2df;',
			'icon-facebook-5' : '&#xe2e0;',
			'icon-facebook-6' : '&#xe2e1;',
			'icon-instagram-2' : '&#xe2e2;',
			'icon-twitter-3' : '&#xe2e3;',
			'icon-twitter-4' : '&#xe2e4;',
			'icon-twitter-5' : '&#xe2e5;',
			'icon-feed-3' : '&#xe2e6;',
			'icon-feed-4' : '&#xe2e7;',
			'icon-feed-5' : '&#xe2e8;',
			'icon-youtube' : '&#xe2e9;',
			'icon-youtube-2' : '&#xe2ea;',
			'icon-vimeo-3' : '&#xe2eb;',
			'icon-vimeo2' : '&#xe2ec;',
			'icon-vimeo-4' : '&#xe2ed;',
			'icon-lanyrd' : '&#xe2ee;',
			'icon-flickr-3' : '&#xe2ef;',
			'icon-flickr-4' : '&#xe2f0;',
			'icon-flickr-5' : '&#xe2f1;',
			'icon-flickr-6' : '&#xe2f2;',
			'icon-picassa' : '&#xe2f3;',
			'icon-picassa-2' : '&#xe2f4;',
			'icon-dribbble-3' : '&#xe2f5;',
			'icon-dribbble-4' : '&#xe2f6;',
			'icon-dribbble-5' : '&#xe2f7;',
			'icon-forrst' : '&#xe2f8;',
			'icon-forrst-2' : '&#xe2f9;',
			'icon-deviantart' : '&#xe2fa;',
			'icon-deviantart-2' : '&#xe2fb;',
			'icon-steam' : '&#xe2fc;',
			'icon-steam-2' : '&#xe2fd;',
			'icon-github-3' : '&#xe2fe;',
			'icon-github-4' : '&#xe2ff;',
			'icon-github-5' : '&#xe300;',
			'icon-github-6' : '&#xe301;',
			'icon-github-7' : '&#xe302;',
			'icon-wordpress' : '&#xe303;',
			'icon-wordpress-2' : '&#xe304;',
			'icon-joomla' : '&#xe305;',
			'icon-blogger' : '&#xe306;',
			'icon-blogger-2' : '&#xe307;',
			'icon-tumblr-3' : '&#xe308;',
			'icon-tumblr-4' : '&#xe309;',
			'icon-yahoo' : '&#xe30a;',
			'icon-tux' : '&#xe30b;',
			'icon-apple' : '&#xe30c;',
			'icon-finder' : '&#xe30d;',
			'icon-android' : '&#xe30e;',
			'icon-windows' : '&#xe30f;',
			'icon-windows8' : '&#xe310;',
			'icon-soundcloud-2' : '&#xe311;',
			'icon-soundcloud-3' : '&#xe312;',
			'icon-skype-3' : '&#xe313;',
			'icon-reddit' : '&#xe314;',
			'icon-linkedin-3' : '&#xe315;',
			'icon-lastfm-3' : '&#xe316;',
			'icon-lastfm-4' : '&#xe317;',
			'icon-delicious' : '&#xe318;',
			'icon-stumbleupon-3' : '&#xe319;',
			'icon-stumbleupon-4' : '&#xe31a;',
			'icon-stackoverflow' : '&#xe31b;',
			'icon-pinterest-3' : '&#xe31c;',
			'icon-pinterest-4' : '&#xe31d;',
			'icon-xing' : '&#xe31e;',
			'icon-xing-2' : '&#xe31f;',
			'icon-flattr-2' : '&#xe320;',
			'icon-foursquare' : '&#xe321;',
			'icon-foursquare-2' : '&#xe322;',
			'icon-paypal-2' : '&#xe323;',
			'icon-paypal-3' : '&#xe324;',
			'icon-paypal-4' : '&#xe325;',
			'icon-yelp' : '&#xe326;',
			'icon-libreoffice' : '&#xe327;',
			'icon-file-pdf' : '&#xe328;',
			'icon-file-openoffice' : '&#xe329;',
			'icon-file-word' : '&#xe32a;',
			'icon-file-excel' : '&#xe32b;',
			'icon-file-zip' : '&#xe32c;',
			'icon-file-powerpoint' : '&#xe32d;',
			'icon-file-xml' : '&#xe32e;',
			'icon-file-css' : '&#xe32f;',
			'icon-html5' : '&#xe330;',
			'icon-html5-2' : '&#xe331;',
			'icon-css3' : '&#xe332;',
			'icon-chrome' : '&#xe333;',
			'icon-firefox' : '&#xe334;',
			'icon-IE' : '&#xe335;',
			'icon-opera' : '&#xe336;',
			'icon-safari' : '&#xe337;',
			'icon-IcoMoon' : '&#xe338;',
			'icon-warning-4' : '&#xe339;',
			'icon-cloud-3' : '&#xe33a;',
			'icon-locked-2' : '&#xe33b;',
			'icon-inbox' : '&#xe33c;',
			'icon-comment-3' : '&#xe33d;',
			'icon-mic' : '&#xe33e;',
			'icon-envelope' : '&#xe33f;',
			'icon-briefcase-3' : '&#xe340;',
			'icon-cart-5' : '&#xe341;',
			'icon-contrast-3' : '&#xe342;',
			'icon-clock-5' : '&#xe343;',
			'icon-user-7' : '&#xe344;',
			'icon-cog-5' : '&#xe345;',
			'icon-music-6' : '&#xe346;',
			'icon-twitter-6' : '&#xe347;',
			'icon-pencil-6' : '&#xe348;',
			'icon-frame' : '&#xe349;',
			'icon-switch-3' : '&#xe34a;',
			'icon-star-7' : '&#xe34b;',
			'icon-key-5' : '&#xe34c;',
			'icon-chart-2' : '&#xe34d;',
			'icon-apple-2' : '&#xe34e;',
			'icon-file-5' : '&#xe34f;',
			'icon-plus-6' : '&#xe350;',
			'icon-minus-6' : '&#xe351;',
			'icon-picture' : '&#xe352;',
			'icon-folder-3' : '&#xe353;',
			'icon-camera-5' : '&#xe354;',
			'icon-search-4' : '&#xe355;',
			'icon-dribbble-6' : '&#xe356;',
			'icon-forrst-3' : '&#xe357;',
			'icon-feed-6' : '&#xe358;',
			'icon-blocked-3' : '&#xe359;',
			'icon-target-3' : '&#xe35a;',
			'icon-play-5' : '&#xe35b;',
			'icon-pause-4' : '&#xe35c;',
			'icon-bug-2' : '&#xe35d;',
			'icon-console-2' : '&#xe35e;',
			'icon-film-2' : '&#xe35f;',
			'icon-type' : '&#xe360;',
			'icon-home-5' : '&#xe361;',
			'icon-earth-3' : '&#xe362;',
			'icon-location-6' : '&#xe363;',
			'icon-info-6' : '&#xe364;',
			'icon-eye-5' : '&#xe365;',
			'icon-heart-6' : '&#xe366;',
			'icon-bookmark-4' : '&#xe367;',
			'icon-wrench-3' : '&#xe368;',
			'icon-calendar-5' : '&#xe369;',
			'icon-window' : '&#xe36a;',
			'icon-monitor' : '&#xe36b;',
			'icon-mobile-5' : '&#xe36c;',
			'icon-droplet-3' : '&#xe36d;',
			'icon-mouse-2' : '&#xe36e;',
			'icon-refresh-3' : '&#xe36f;',
			'icon-location-7' : '&#xe370;',
			'icon-tag-4' : '&#xe371;',
			'icon-phone-4' : '&#xe372;',
			'icon-star-8' : '&#xe373;',
			'icon-pointer' : '&#xe374;',
			'icon-thumbs-up-5' : '&#xe375;',
			'icon-thumbs-down-3' : '&#xe376;',
			'icon-headphones-2' : '&#xe377;',
			'icon-move-2' : '&#xe378;',
			'icon-checkmark-5' : '&#xe379;',
			'icon-cancel-2' : '&#xe37a;',
			'icon-skype-4' : '&#xe37b;',
			'icon-gift-2' : '&#xe37c;',
			'icon-cone-2' : '&#xe37d;',
			'icon-alarm-3' : '&#xe37e;',
			'icon-coffee' : '&#xe37f;',
			'icon-basket' : '&#xe380;',
			'icon-flag-4' : '&#xe381;',
			'icon-ipod' : '&#xe382;',
			'icon-trashcan-2' : '&#xe383;',
			'icon-bolt-2' : '&#xe384;',
			'icon-ampersand' : '&#xe385;',
			'icon-compass-4' : '&#xe386;',
			'icon-list-7' : '&#xe387;',
			'icon-grid-2' : '&#xe388;',
			'icon-volume-3' : '&#xe389;',
			'icon-volume-4' : '&#xe38a;',
			'icon-stats-3' : '&#xe38b;',
			'icon-target-4' : '&#xe38c;',
			'icon-forward-6' : '&#xe38d;',
			'icon-paperclip-2' : '&#xe38e;',
			'icon-keyboard-3' : '&#xe38f;',
			'icon-crop-2' : '&#xe390;',
			'icon-floppy' : '&#xe391;',
			'icon-filter-3' : '&#xe392;',
			'icon-trophy-3' : '&#xe393;',
			'icon-diary' : '&#xe394;',
			'icon-address-book-2' : '&#xe395;',
			'icon-stop-4' : '&#xe396;',
			'icon-smiley-3' : '&#xe397;',
			'icon-shit' : '&#xe398;',
			'icon-bookmark-5' : '&#xe399;',
			'icon-camera-6' : '&#xe39a;',
			'icon-lamp' : '&#xe39b;',
			'icon-disk-3' : '&#xe39c;',
			'icon-button' : '&#xe39d;',
			'icon-database-2' : '&#xe39e;',
			'icon-credit-card-2' : '&#xe39f;',
			'icon-atom' : '&#xe3a0;',
			'icon-winsows' : '&#xe3a1;',
			'icon-target-5' : '&#xe3a2;',
			'icon-battery-3' : '&#xe3a3;',
			'icon-code-3' : '&#xe3a4;',
			'icon-sunrise' : '&#xe3a5;',
			'icon-sun-4' : '&#xe3a6;',
			'icon-moon-2' : '&#xe3a7;',
			'icon-sun-5' : '&#xe3a8;',
			'icon-windy' : '&#xe3a9;',
			'icon-wind' : '&#xe3aa;',
			'icon-snowflake' : '&#xe3ab;',
			'icon-cloudy' : '&#xe3ac;',
			'icon-cloud-4' : '&#xe3ad;',
			'icon-weather' : '&#xe3ae;',
			'icon-weather-2' : '&#xe3af;',
			'icon-weather-3' : '&#xe3b0;',
			'icon-lines' : '&#xe3b1;',
			'icon-cloud-5' : '&#xe3b2;',
			'icon-lightning-2' : '&#xe3b3;',
			'icon-lightning-3' : '&#xe3b4;',
			'icon-rainy' : '&#xe3b5;',
			'icon-rainy-2' : '&#xe3b6;',
			'icon-windy-2' : '&#xe3b7;',
			'icon-windy-3' : '&#xe3b8;',
			'icon-snowy' : '&#xe3b9;',
			'icon-snowy-2' : '&#xe3ba;',
			'icon-snowy-3' : '&#xe3bb;',
			'icon-weather-4' : '&#xe3bc;',
			'icon-cloudy-2' : '&#xe3bd;',
			'icon-cloud-6' : '&#xe3be;',
			'icon-lightning-4' : '&#xe3bf;',
			'icon-sun-6' : '&#xe3c0;',
			'icon-moon-3' : '&#xe3c1;',
			'icon-cloudy-3' : '&#xe3c2;',
			'icon-cloud-7' : '&#xe3c3;',
			'icon-cloud-8' : '&#xe3c4;',
			'icon-lightning-5' : '&#xe3c5;',
			'icon-rainy-3' : '&#xe3c6;',
			'icon-rainy-4' : '&#xe3c7;',
			'icon-windy-4' : '&#xe3c8;',
			'icon-windy-5' : '&#xe3c9;',
			'icon-snowy-4' : '&#xe3ca;',
			'icon-snowy-5' : '&#xe3cb;',
			'icon-weather-5' : '&#xe3cc;',
			'icon-cloudy-4' : '&#xe3cd;',
			'icon-lightning-6' : '&#xe3ce;',
			'icon-thermometer-2' : '&#xe3cf;',
			'icon-compass-5' : '&#xe3d0;',
			'icon-none' : '&#xe3d1;',
			'icon-Celsius' : '&#xe3d2;',
			'icon-Fahrenheit' : '&#xe3d3;',
			'icon-chat-3' : '&#xe3d4;',
			'icon-chat-alt-stroke' : '&#xe3d5;',
			'icon-chat-alt-fill' : '&#xe3d6;',
			'icon-comment-alt1-stroke' : '&#xe3d7;',
			'icon-comment-alt1-fill' : '&#xe3d8;',
			'icon-comment-stroke' : '&#xe3d9;',
			'icon-comment-fill' : '&#xe3da;',
			'icon-comment-alt2-stroke' : '&#xe3db;',
			'icon-comment-alt2-fill' : '&#xe3dc;',
			'icon-checkmark-6' : '&#xe3dd;',
			'icon-check-alt' : '&#xe3de;',
			'icon-x' : '&#xe3df;',
			'icon-x-altx-alt' : '&#xe3e0;',
			'icon-denied' : '&#xe3e1;',
			'icon-cursor' : '&#xe3e2;',
			'icon-rss-2' : '&#xe3e3;',
			'icon-rss-alt' : '&#xe3e4;',
			'icon-wrench-4' : '&#xe3e5;',
			'icon-dial' : '&#xe3e6;',
			'icon-cog-6' : '&#xe3e7;',
			'icon-calendar-6' : '&#xe3e8;',
			'icon-calendar-alt-stroke' : '&#xe3e9;',
			'icon-calendar-alt-fill' : '&#xe3ea;',
			'icon-share-3' : '&#xe3eb;',
			'icon-mail-7' : '&#xe3ec;',
			'icon-heart-stroke' : '&#xe3ed;',
			'icon-heart-fill' : '&#xe3ee;',
			'icon-movie' : '&#xe3ef;',
			'icon-document-alt-stroke' : '&#xe3f0;',
			'icon-document-alt-fill' : '&#xe3f1;',
			'icon-document-stroke' : '&#xe3f2;',
			'icon-document-fill' : '&#xe3f3;',
			'icon-plus-7' : '&#xe3f4;',
			'icon-plus-alt' : '&#xe3f5;',
			'icon-minus-7' : '&#xe3f6;',
			'icon-minus-alt' : '&#xe3f7;',
			'icon-pin-2' : '&#xe3f8;',
			'icon-link-3' : '&#xe3f9;',
			'icon-bolt-3' : '&#xe3fa;',
			'icon-move-3' : '&#xe3fb;',
			'icon-move-alt1' : '&#xe3fc;',
			'icon-move-alt2' : '&#xe3fd;',
			'icon-equalizer-2' : '&#xe3fe;',
			'icon-award-fill' : '&#xe3ff;',
			'icon-award-stroke' : '&#xe400;',
			'icon-magnifying-glass' : '&#xe401;',
			'icon-trash-stroke' : '&#xe402;',
			'icon-trash-fill' : '&#xe403;',
			'icon-beaker-alt' : '&#xe404;',
			'icon-beaker' : '&#xe405;',
			'icon-key-stroke' : '&#xe406;',
			'icon-key-fill' : '&#xe407;',
			'icon-new-window' : '&#xe408;',
			'icon-lightbulb' : '&#xe409;',
			'icon-spin-alt' : '&#xe40a;',
			'icon-spin' : '&#xe40b;',
			'icon-curved-arrow' : '&#xe40c;',
			'icon-undo-3' : '&#xe40d;',
			'icon-reload' : '&#xe40e;',
			'icon-reload-alt' : '&#xe40f;',
			'icon-loop-6' : '&#xe410;',
			'icon-loop-alt1' : '&#xe411;',
			'icon-loop-alt2' : '&#xe412;',
			'icon-loop-alt3' : '&#xe413;',
			'icon-loop-alt4' : '&#xe414;',
			'icon-transfer' : '&#xe415;',
			'icon-move-vertical' : '&#xe416;',
			'icon-move-vertical-alt1' : '&#xe417;',
			'icon-move-vertical-alt2' : '&#xe418;',
			'icon-move-horizontal' : '&#xe419;',
			'icon-move-horizontal-alt1' : '&#xe41a;',
			'icon-move-horizontal-alt2' : '&#xe41b;',
			'icon-arrow-left-14' : '&#xe41c;',
			'icon-arrow-left-alt1' : '&#xe41d;',
			'icon-arrow-left-alt2' : '&#xe41e;',
			'icon-arrow-right-13' : '&#xe41f;',
			'icon-arrow-right-alt1' : '&#xe420;',
			'icon-arrow-right-alt2' : '&#xe421;',
			'icon-arrow-up-12' : '&#xe422;',
			'icon-arrow-up-alt1' : '&#xe423;',
			'icon-arrow-up-alt2' : '&#xe424;',
			'icon-arrow-down-13' : '&#xe425;',
			'icon-arrow-down-alt1' : '&#xe426;',
			'icon-arrow-down-alt2' : '&#xe427;',
			'icon-cd-2' : '&#xe428;',
			'icon-steering-wheel' : '&#xe429;',
			'icon-microphone-3' : '&#xe42a;',
			'icon-headphones-3' : '&#xe42b;',
			'icon-volume-5' : '&#xe42c;',
			'icon-volume-mute-3' : '&#xe42d;',
			'icon-play-6' : '&#xe42e;',
			'icon-pause-5' : '&#xe42f;',
			'icon-stop-5' : '&#xe430;',
			'icon-eject-3' : '&#xe431;',
			'icon-first-3' : '&#xe432;',
			'icon-last-3' : '&#xe433;',
			'icon-play-alt' : '&#xe434;',
			'icon-fullscreen-exit' : '&#xe435;',
			'icon-fullscreen-exit-alt' : '&#xe436;',
			'icon-fullscreen' : '&#xe437;',
			'icon-fullscreen-alt' : '&#xe438;',
			'icon-iphone' : '&#xe439;',
			'icon-battery-empty' : '&#xe43a;',
			'icon-battery-half' : '&#xe43b;',
			'icon-battery-full-2' : '&#xe43c;',
			'icon-battery-charging-2' : '&#xe43d;',
			'icon-compass-6' : '&#xe43e;',
			'icon-box-2' : '&#xe43f;',
			'icon-folder-stroke' : '&#xe440;',
			'icon-folder-fill' : '&#xe441;',
			'icon-at' : '&#xe442;',
			'icon-ampersand-2' : '&#xe443;',
			'icon-info-7' : '&#xe444;',
			'icon-question-mark' : '&#xe445;',
			'icon-pilcrow-2' : '&#xe446;',
			'icon-hash' : '&#xe447;',
			'icon-left-quote' : '&#xe448;',
			'icon-right-quote' : '&#xe449;',
			'icon-left-quote-alt' : '&#xe44a;',
			'icon-right-quote-alt' : '&#xe44b;',
			'icon-article' : '&#xe44c;',
			'icon-read-more' : '&#xe44d;',
			'icon-list-8' : '&#xe44e;',
			'icon-list-nested' : '&#xe44f;',
			'icon-book-4' : '&#xe450;',
			'icon-book-alt' : '&#xe451;',
			'icon-book-alt2' : '&#xe452;',
			'icon-pen-2' : '&#xe453;',
			'icon-pen-alt-stroke' : '&#xe454;',
			'icon-pen-alt-fill' : '&#xe455;',
			'icon-pen-alt2' : '&#xe456;',
			'icon-brush-2' : '&#xe457;',
			'icon-brush-alt' : '&#xe458;',
			'icon-eyedropper' : '&#xe459;',
			'icon-layers-alt' : '&#xe45a;',
			'icon-layers' : '&#xe45b;',
			'icon-image-3' : '&#xe45c;',
			'icon-camera-7' : '&#xe45d;',
			'icon-aperture' : '&#xe45e;',
			'icon-aperture-alt' : '&#xe45f;',
			'icon-chart-3' : '&#xe460;',
			'icon-chart-alt' : '&#xe461;',
			'icon-bars-5' : '&#xe462;',
			'icon-bars-alt' : '&#xe463;',
			'icon-eye-6' : '&#xe464;',
			'icon-user-8' : '&#xe465;',
			'icon-home-6' : '&#xe466;',
			'icon-clock-6' : '&#xe467;',
			'icon-lock-stroke' : '&#xe468;',
			'icon-lock-fill' : '&#xe469;',
			'icon-unlock-stroke' : '&#xe46a;',
			'icon-unlock-fill' : '&#xe46b;',
			'icon-tag-stroke' : '&#xe46c;',
			'icon-tag-fill' : '&#xe46d;',
			'icon-sun-stroke' : '&#xe46e;',
			'icon-sun-fill' : '&#xe46f;',
			'icon-moon-stroke' : '&#xe470;',
			'icon-moon-fill' : '&#xe471;',
			'icon-cloud-9' : '&#xe472;',
			'icon-rain' : '&#xe473;',
			'icon-umbrella' : '&#xe474;',
			'icon-star-9' : '&#xe475;',
			'icon-map-pin-stroke' : '&#xe476;',
			'icon-map-pin-fill' : '&#xe477;',
			'icon-map-pin-alt' : '&#xe478;',
			'icon-target-6' : '&#xe479;',
			'icon-download-6' : '&#xe47a;',
			'icon-upload-6' : '&#xe47b;',
			'icon-cloud-download-2' : '&#xe47c;',
			'icon-cloud-upload-2' : '&#xe47d;',
			'icon-fork' : '&#xe47e;',
			'icon-paperclip-3' : '&#xe47f;'
		},
		els = document.getElementsByTagName('*'),
		i, attr, html, c, el;
	for (i = 0; i < els.length; i += 1) {
		el = els[i];
		attr = el.getAttribute('data-icon');
		if (attr) {
			addIcon(el, attr);
		}
		c = el.className;
		c = c.match(/icon-[^\s'"]+/);
		if (c && icons[c[0]]) {
			addIcon(el, icons[c[0]]);
		}
	}
};
/* ==========================================================
 * bootpanorama-panorama.js v1.0
 * http://aozora.github.com/bootpanorama/
 * ==========================================================
 * Copyright 2012 Marcello Palmitessa
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * ========================================================== */


!function ($) {

   "use strict";


   /* PIVOT CLASS DEFINITION
    * ========================= */

   var Panorama = function (element, options) {
      this.$element = $(element)
      this.options = options
//      this.options.slide && this.slide(this.options.slide)

      this.$groups = $('.panorama-sections .panorama-section')
      this.$current = 0

      this.init()
   }

   Panorama.prototype = {

      // init panorama workspace
      init: function(){
         var $this = this

         // arrange the panorama height
         this.$element.height( this.$element.parent().height() - $('#nav-bar').outerHeight() )

         // arrange the section container width
         $this.resize();

         // setup mousewheel
         // win8: wheel-down => scroll to right -1 0 -1
         //       wheel-up   => scroll to left   1 0  1
         if (this.options.mousewheel){
            $('.panorama-sections').mousewheel(function(event, delta, deltaX, deltaY) {
               //e.preventDefault();
               //console.log(delta, deltaX, deltaY);
               if (delta > 0){
                  $this.prev()
               }
               else{
                  $this.next()
               }
            });
         }

         // Arrange Tiles like Win8 ones
         if (this.options.arrangetiles){
            $('.panorama-sections .panorama-section').each(function(index, el){




            });
         }


         // parallax can be activated only if there is CSS3 transition support
         if (this.options.parallax){
            // add a class to enable css3 transition
            $('body').addClass("panorama-parallax");
         }

         if (this.options.showscrollbuttons){
            var $p = $('.panorama-sections');

            $('#panorama-scroll-prev').click(function(e){
               e.preventDefault();
               $this.prev()
            });

            $("#panorama-scroll-next").click(function(e){
               e.preventDefault();
               $this.next()
            });
         } else {
            $('#panorama-scroll-prev').hide()
            $('#panorama-scroll-next').hide()
         }


         //Enable swiping...
         $(".panorama").swipe( {
            //Generic swipe handler for all directions
            swipe:function(event, direction, distance, duration, fingerCount) {
               if (direction=='right'){
                  $this.prev()
               }
               if (direction=='left'){
                  $this.next()
               }
            }
            ,threshold: 0
            //ingers: 'all'
         });

         $this.setButtons()


         if (this.options.keyboard){
            $(document).on('keyup', function ( e ) {
               if (e.which == 37) { // left-arrow
                  $this.prev()
               }

               if (e.which == 39) { // right-arrow
                  $this.next()
               }
            })
         }


//         $(window).resize(function() {
//
//            // call resize function
//
//         });

      } // end init

      , next: function () {
         var $this = this
         this.$current++
         if (this.$current >= this.$groups.length){
            this.$current = this.$groups.length - 1
         }

         var $p = $('.panorama-sections');
         var targetOffset = $(this.$groups[this.$current]).position().left


         if (this.options.parallax && $.support.transition){
            $('body').css('background-position', (targetOffset / 2) + 'px 0px')
         }

         $p.animate({ marginLeft: -targetOffset },
            {
               duration: 200,
               easing: 'swing'
               ,complete: function(){$this.setButtons()}
            }
         );

      }

      , prev: function () {
         var $this = this
         this.$current--
         if (this.$current < 0){
            this.$current = 0
         }

         var $p = $('.panorama-sections');
         var targetOffset = $(this.$groups[this.$current]).position().left

         if (this.options.parallax && $.support.transition){
            $('body').css('background-position', (targetOffset / 2) + 'px 0px')
         }

         $p.animate({
               marginLeft: -targetOffset
            },
            {
               duration: 200,
               easing: 'swing'
               ,complete: function(){$this.setButtons()}
            }
         );

      }

      , resize: function(){
         // arrange the section container width
         var totalWidth = 0
         $('.panorama-sections .panorama-section').each(function(index, el){
            totalWidth += $(el).outerWidth(true)
         });
         //rei
         //$('.panorama-sections')
         //   .width(totalWidth)
         //   .height( this.$element.parent().height())

      }

      , setButtons: function () {

         if (!this.options.showscrollbuttons){
            return false;
         }

         if (this.$current === 0){
            $("#panorama-scroll-prev").hide();
         } else {
            $("#panorama-scroll-prev").show();
         }

         if (this.$current === this.$groups.length - 1){
            $("#panorama-scroll-next").hide();
         } else {
            $("#panorama-scroll-next").show();
         }
      }

   }


   /* PANORAMA PLUGIN DEFINITION
    * ========================== */

   $.fn.panorama = function (option) {
      return this.each(function () {
         var $this = $(this)
            , data = $this.data('panorama')
            , options = $.extend({}, $.fn.panorama.defaults, typeof option == 'object' && option)
            , action = typeof option == 'string' ? option : options.slide
         if (!data) {
            $this.data('panorama', (data = new Panorama(this, options)))
         }
//         if (typeof option == 'number') data.to(option)
         else if (action){
            data[action]()
         }

      })
   }

   $.fn.panorama.defaults = {
      showscrollbuttons: true,
      parallax: false,
      keyboard: true,
      mousewheel: true,
      arrangetiles: true
   }

   $.fn.panorama.Constructor = Panorama

}(window.jQuery);

/* ==========================================================
 * bootstrap-pivot.js v1.0.0 alpha1
 * http://aozora.github.com/bootmetro/
 * ==========================================================
 * Copyright 2013 Marcello Palmitessa
 * ========================================================== */


!function ($) {

   "use strict";


   /* PIVOT CLASS DEFINITION
    * ========================= */

   var Pivot = function (element, options) {
      this.$element = $(element)
      this.options = options
      this.options.slide && this.slide(this.options.slide)
   }




   Pivot.prototype = {

      to: function (pos) {
         var $active = this.$element.find('> .pivot-items > .pivot-item.active').eq(0)
            , children = $active.parent().children()
            , activePos = children.index($active)
            , that = this

         if (pos > (children.length - 1) || pos < 0)
            return

         if (this.sliding) {
            return this.$element.one('slid', function () {
               that.to(pos)
            })
            return
         }

         if (pos === activePos)
            return

         return this.slide(pos > activePos ? 'next' : 'prev', $(children[pos]))
      }

      , next: function () {
         if (this.sliding) return
         return this.slide('next')
      }

      , prev: function () {
         if (this.sliding) return
         return this.slide('prev')
      }

      , slide: function (type, next) {
         var $active = this.$element.find('> .pivot-items > .pivot-item.active').eq(0)
            , $next = next || $active[type]()
            , direction = type == 'next' ? 'left' : 'right'
            , fallback  = type == 'next' ? 'first' : 'last'
            , that = this
            , e

         this.sliding = true

         $next = $next.length ? $next : this.$element.find('> .pivot-items > .pivot-item')[fallback]()

         e = $.Event('slide', {
            relatedTarget: $next[0]
         })

         if ($next.hasClass('active')) {
            that.sliding = false // this should prevent issue #45
            return
         }

         if ($.support.transition && this.$element.hasClass('slide')) {
            this.$element.trigger(e)
            if (e.isDefaultPrevented())
               return

            $next.addClass(type)
            $next[0].offsetWidth // force reflow
            $active.addClass(direction)
            $next.addClass(direction)
            this.$element.one($.support.transition.end, function () {
               $next.removeClass([type, direction].join(' ')).addClass('active')
               $active.removeClass(['active', direction].join(' '))
               that.sliding = false
               setTimeout(function () { that.$element.trigger('slid') }, 0)
            })
         } else {
            this.$element.trigger(e)
            if (e.isDefaultPrevented())
               return

            $active.removeClass('active')
            $next.addClass('active')
            this.sliding = false
            this.$element.trigger('slid')
         }

         return this
      }

   }


   /* PIVOT PLUGIN DEFINITION
    * ========================== */

   $.fn.pivot = function (option) {
      return this.each(function () {
         var $this = $(this)
            , data = $this.data('pivot')
            , options = $.extend({}, $.fn.pivot.defaults, typeof option == 'object' && option)
            , action = typeof option == 'string' ? option : options.slide
         if (!data)
            $this.data('pivot', (data = new Pivot(this, options)))
         if (typeof option == 'number')
            data.to(option)
         else if (action) data[action]()
      })
   }

   $.fn.pivot.defaults = { }

   $.fn.pivot.Constructor = Pivot



   /* PIVOT DATA-API
    * ================= */

   $(document).on('click.pivot.data-api', '[data-pivot-index]', function (e) {
      var $this = $(this), href
         , $target = $($this.attr('data-target') || (href = $this.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '')) //strip for ie7
         , options = $.extend({}, $target.data(), $this.data())
         , $index = parseInt($this.attr('data-pivot-index'), 10);

      $('[data-pivot-index].active').removeClass('active')
      $this.addClass('active')

      $target.pivot($index)
      e.preventDefault()
   })


}(window.jQuery);
