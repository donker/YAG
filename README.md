## Project Description
A DotNetNuke (photo) gallery module based on templating. Basic photo management implementation. No subfolders, just a straight list of pictures. Perfect if you need to include a small gallery on a DNN page. Comes complete with a number of lightbox and rotator templates.

As so many projects this grew out of a discrepancy between what I needed and what was out there. The features of this module are:
- Templated output of a list of images using simple HTML templates but powerful enough to use existing jQuery lightbox solutions to render this
- Simple management of a single list (i.e. no hierarchy) of images
- Ability to regenerate the thumbnails in case you want them a different size **after** having uploaded the images already (this means keeping the original)
- jQuery uploader for smooth uploading of multiple files at once
- Edit screen with drag and drop support to help reorder pictures

The templating solution is quite elaborate and illustrates, IMHO, how DotNetNuke's so-called token replace mechanism can be enhanced to include iteration. I've also added a mechanism to allow for a collection of settings for each template that can then be edited by the editor.

Included (jQuery) templates are:
- [prettyPhoto](http://www.no-margin-for-errors.com/projects/prettyphoto-jquery-lightbox-clone/) (default)
- [colorbox](http://designresourcebox.com/snippet/colorbox-lightbox-plugin/)
- [cycle](http://jquery.malsup.com/cycle/)
- [fancybox](http://fancybox.net/)
- [NFLightbox](http://www.notesfor.net/post/NotesForLightBox.aspx)
- [Slimbox2](http://www.digitalia.be/software/slimbox2)
- [GalleryView](http://spaceforaname.com/galleryview)
- [Masonry](http://masonry.desandro.com)
Note I cannot guarantee they will all play nice with your site. A common cause of jQuery issues is clashes between installed components and/or the used skin. If you have issues you'll need to debug yourself using tools like Firebug.

## Project Sponsor

![Bring2mind](http://www.bring2mind.net/Portals/0/images/Logo01.png)

This project was donated by [Bring2mind](http://www.bring2mind.net), makers of DotNetNuke's premier document management module [Document Exchange](http://www.bring2mind.net/DocumentExchange/Overview.aspx). The programmer is Peter Donker,  [DotNetNuke MVP](http://www.dnnsoftware.com/Community/Connect/DNN-MVP).
