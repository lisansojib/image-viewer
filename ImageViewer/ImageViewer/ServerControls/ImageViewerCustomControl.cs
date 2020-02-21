using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImageViewer
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ImageViewerCustomControl runat=server></{0}:ImageViewerCustomControl>")]
    public class ImageViewerCustomControl : WebControl
    {
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Text
        {
            get
            {
                string s = (string)ViewState["Text"];
                return (s ?? string.Empty);
            }
            set
            {
                ViewState["Text"] = value;
            }
        }

        protected override void RenderContents(HtmlTextWriter output)
        {
            output.WriteLine("<link href='/web/viewer.css' rel='stylesheet' />");
            output.WriteLine("<link rel='resource' type='application/l10n' href='/web/locale/locale.properties'>");
            output.WriteLine("<script src='/build/pdf.js'></script>");
            output.WriteLine("<script src='/web/viewer.js'></script>");
            output.WriteLine();
            output.WriteLine("<div tabindex='1' class='loadingInProgress'>");

            #region outerContainer
            output.WriteLine("<div id='outerContainer'>");

            #region sidebarContainer
            output.WriteLine("<div id='sidebarContainer'>");

            output.WriteLine("<div id='toolbarSidebar'>");
            output.WriteLine("<div class='splitToolbarButton toggled'>");
            output.WriteLine("<button id='viewThumbnail' class='toolbarButton toggled' title='Show Thumbnails' tabindex='2' data-l10n-id='thumbs'><span data-l10n-id='thumbs_label'>Thumbnails</span></button>");
            output.WriteLine("<button id='viewOutline' class='toolbarButton' title='Show Document Outline(double-click to expand/collapse all items)' tabindex='3' data-l10n-id='document_outline'><span data-l10n-id='document_outline_label'>Document Outline</span></button>");
            output.WriteLine("<button id='viewAttachments' class='toolbarButton' title='Show Attachments' tabindex='4' data-l10n-id='attachments'><span data-l10n-id='attachments_label'>Attachments</span></button>");
            output.WriteLine("</div>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='sidebarContent'><div id='thumbnailView'></div><div id='outlineView' class='hidden'></div><div id='attachmentsView' class='hidden'></div></div>");

            output.WriteLine("<div id='sidebarResizer' class='hidden'></div>");

            output.WriteLine("</div>");
            #endregion

            #region mainContainer
            output.WriteLine("<div id='mainContainer'>");

            #region Findbar
            output.WriteLine("<div class='findbar hidden doorHanger' id='findbar'>");

            output.WriteLine("<div id='findbarInputContainer'>");
            output.WriteLine("<input id='findInput' class='toolbarField' title='Find' placeholder='Find in document…' tabindex='91' data-l10n-id='find_input'>");
            output.WriteLine("<div class='splitToolbarButton'>");
            output.WriteLine("<button id='findPrevious' class='toolbarButton findPrevious' title='Find the previous occurrence of the phrase' tabindex='92' data-l10n-id='find_previous'><span data-l10n-id='find_previous_label'>Previous</span></button>");
            output.WriteLine("<div class='splitToolbarButtonSeparator'></div>");
            output.WriteLine("<button id='findNext' class='toolbarButton findNext' title='Find the next occurrence of the phrase' tabindex='93' data-l10n-id='find_next'><span data-l10n-id='find_next_label'>Next</span></button>");
            output.WriteLine("</div>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='findbarOptionsOneContainer'>");
            output.WriteLine("<input type='checkbox' id='findHighlightAll' class='toolbarField' tabindex='94'>");
            output.WriteLine("<label for='findHighlightAll' class='toolbarLabel' data-l10n-id='find_highlight'>Highlight all</label>");
            output.WriteLine("<input type='checkbox' id='findMatchCase' class='toolbarField' tabindex='95'>");
            output.WriteLine("<label for='findMatchCase' class='toolbarLabel' data-l10n-id='find_match_case_label'>Match case</label>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='findbarOptionsTwoContainer'>");
            output.WriteLine("<input type='checkbox' id='findEntireWord' class='toolbarField' tabindex='96'>");
            output.WriteLine("<label for='findEntireWord' class='toolbarLabel' data-l10n-id='find_entire_word_label'>Whole words</label>");
            output.WriteLine("<span id='findResultsCount' class='toolbarLabel hidden'></span>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='findbarMessageContainer'>");
            output.WriteLine("<span id='findMsg' class='toolbarLabel'></span>");
            output.WriteLine("</div>");

            output.WriteLine("</div>"); // findBar
            #endregion

            #region secondaryToolbar
            output.WriteLine("<div id='secondaryToolbar' class='secondaryToolbar hidden doorHangerRight'>");
            output.WriteLine("<div id='secondaryToolbarButtonContainer'>");
            output.WriteLine("<button id='secondaryPresentationMode' class='secondaryToolbarButton presentationMode visibleLargeView' title='Switch to Presentation Mode' tabindex='51' data-l10n-id='presentation_mode'><span data-l10n-id='presentation_mode_label'>Presentation Mode</span></button>");
            output.WriteLine("<button id='secondaryOpenFile' class='secondaryToolbarButton openFile visibleLargeView' title='Open File' tabindex='52' data-l10n-id='open_file'><span data-l10n-id='open_file_label'>Open</span></button>");
            output.WriteLine("<button id='secondaryPrint' class='secondaryToolbarButton print visibleMediumView' title='Print' tabindex='53' data-l10n-id='print'><span data-l10n-id='print_label'>Print</span></button>");
            output.WriteLine("<button id='secondaryDownload' class='secondaryToolbarButton download visibleMediumView' title='Download' tabindex='54' data-l10n-id='download'><span data-l10n-id='download_label'>Download</span></button>");
            output.WriteLine("<a href='#' id='secondaryViewBookmark' class='secondaryToolbarButton bookmark visibleSmallView' title='Current view (copy or open in new window)' tabindex='55' data-l10n-id='bookmark'><span data-l10n-id='bookmark_label'>Current View</span></a>");
            output.WriteLine("<div class='horizontalToolbarSeparator visibleLargeView'></div>");
            output.WriteLine("<button id='firstPage' class='secondaryToolbarButton firstPage' title='Go to First Page' tabindex='56' data-l10n-id='first_page'><span data-l10n-id='first_page_label'>Go to First Page</span></button>");
            output.WriteLine("<button id='lastPage' class='secondaryToolbarButton lastPage' title='Go to Last Page' tabindex='57' data-l10n-id='last_page'><span data-l10n-id='last_page_label'>Go to Last Page</span></button>");
            output.WriteLine("<div class='horizontalToolbarSeparator'></div>");
            output.WriteLine("<button id='pageRotateCw' class='secondaryToolbarButton rotateCw' title='Rotate Clockwise' tabindex='58' data-l10n-id='page_rotate_cw'><span data-l10n-id='page_rotate_cw_label'>Rotate Clockwise</span></button>");
            output.WriteLine("<button id='pageRotateCcw' class='secondaryToolbarButton rotateCcw' title='Rotate Counterclockwise' tabindex='59' data-l10n-id='page_rotate_ccw'><span data-l10n-id='page_rotate_ccw_label'>Rotate Counterclockwise</span></button>");
            output.WriteLine("<div class='horizontalToolbarSeparator'></div>");
            output.WriteLine("<button id='cursorSelectTool' class='secondaryToolbarButton selectTool toggled' title='Enable Text Selection Tool' tabindex='60' data-l10n-id='cursor_text_select_tool'><span data-l10n-id='cursor_text_select_tool_label'>Text Selection Tool</span></button>");
            output.WriteLine("<button id='cursorHandTool' class='secondaryToolbarButton handTool' title='Enable Hand Tool' tabindex='61' data-l10n-id='cursor_hand_tool'><span data-l10n-id='cursor_hand_tool_label'>Hand Tool</span></button>");
            output.WriteLine("<div class='horizontalToolbarSeparator'></div>");
            output.WriteLine("<button id='scrollVertical' class='secondaryToolbarButton scrollModeButtons scrollVertical toggled' title='Use Vertical Scrolling' tabindex='62' data-l10n-id='scroll_vertical'><span data-l10n-id='scroll_vertical_label'>Vertical Scrolling</span></button>");
            output.WriteLine("<button id='scrollHorizontal' class='secondaryToolbarButton scrollModeButtons scrollHorizontal' title='Use Horizontal Scrolling' tabindex='63' data-l10n-id='scroll_horizontal'><span data-l10n-id='scroll_horizontal_label'>Horizontal Scrolling</span></button>");
            output.WriteLine("<button id='scrollWrapped' class='secondaryToolbarButton scrollModeButtons scrollWrapped' title='Use Wrapped Scrolling' tabindex='64' data-l10n-id='scroll_wrapped'><span data-l10n-id='scroll_wrapped_label'>Wrapped Scrolling</span></button>");
            output.WriteLine("<div class='horizontalToolbarSeparator scrollModeButtons'></div>");
            output.WriteLine("<button id='spreadNone' class='secondaryToolbarButton spreadModeButtons spreadNone toggled' title='Do not join page spreads' tabindex='65' data-l10n-id='spread_none'><span data-l10n-id='spread_none_label'>No Spreads</span></button>");
            output.WriteLine("<button id='spreadOdd' class='secondaryToolbarButton spreadModeButtons spreadOdd' title='Join page spreads starting with odd-numbered pages' tabindex='66' data-l10n-id='spread_odd'><span data-l10n-id='spread_odd_label'>Odd Spreads</span></button>");
            output.WriteLine("<button id='spreadEven' class='secondaryToolbarButton spreadModeButtons spreadEven' title='Join page spreads starting with even-numbered pages' tabindex='67' data-l10n-id='spread_even'><span data-l10n-id='spread_even_label'>Even Spreads</span></button>");
            output.WriteLine("<div class='horizontalToolbarSeparator spreadModeButtons'></div>");
            output.WriteLine("<button id='documentProperties' class='secondaryToolbarButton documentProperties' title='Document Properties…' tabindex='68' data-l10n-id='document_properties'><span data-l10n-id='document_properties_label'>Document Properties…</span></button>");
            output.WriteLine("</div>");
            output.WriteLine("</div>"); //secondaryToolbar
            #endregion

            #region toolbar
            output.WriteLine("<div class='toolbar'>");
            output.WriteLine("<div id='toolbarContainer'>");

            output.WriteLine("<div id='toolbarViewer'>");
            output.WriteLine("<div id='toolbarViewerLeft'>");
            output.WriteLine("<button id='sidebarToggle' class='toolbarButton' title='Toggle Sidebar' tabindex='11' data-l10n-id='toggle_sidebar'><span data-l10n-id='toggle_sidebar_label'>Toggle Sidebar</span></button>");
            output.WriteLine("<div class='toolbarButtonSpacer'></div>");
            output.WriteLine("<button id='viewFind' class='toolbarButton' title='Find in Document' tabindex='12' data-l10n-id='findbar'><span data-l10n-id='findbar_label'>Find</span></button>");
            output.WriteLine("<div class='splitToolbarButton hiddenSmallView'>");
            output.WriteLine("<button class='toolbarButton pageUp' title='Previous Page' id='previous' tabindex='13' data-l10n-id='previous'><span data-l10n-id='previous_label'>Previous</span></button>");
            output.WriteLine("<div class='splitToolbarButtonSeparator'></div>");
            output.WriteLine("<button class='toolbarButton pageDown' title='Next Page' id='next' tabindex='14' data-l10n-id='next'><span data-l10n-id='next_label'>Next</span></button>");
            output.WriteLine("</div>");
            output.WriteLine("<input type='number' id='pageNumber' class='toolbarField pageNumber' title='Page' value='1' size='4' min='1' tabindex='15' data-l10n-id='page'>");
            output.WriteLine("<span id='numPages' class='toolbarLabel'></span>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='toolbarViewerRight'>");
            output.WriteLine("<button id='presentationMode' class='toolbarButton presentationMode hiddenLargeView' title='Switch to Presentation Mode' tabindex='31' data-l10n-id='presentation_mode'><span data-l10n-id='presentation_mode_label'>Presentation Mode</span></button>");
            output.WriteLine("<button id='openFile' class='toolbarButton openFile hiddenLargeView' title='Open File' tabindex='32' data-l10n-id='open_file'><span data-l10n-id='open_file_label'>Open</span></button>");
            output.WriteLine("<button id='print' class='toolbarButton print hiddenMediumView' title='Print' tabindex='33' data-l10n-id='print'><span data-l10n-id='print_label'>Print</span></button>");
            output.WriteLine("<button id='download' class='toolbarButton download hiddenMediumView' title='Download' tabindex='34' data-l10n-id='download'><span data-l10n-id='download_label'>Download</span></button>");
            output.WriteLine("<a href='#' id='viewBookmark' class='toolbarButton bookmark hiddenSmallView' title='Current view (copy or open in new window)' tabindex='35' data-l10n-id='bookmark'><span data-l10n-id='bookmark_label'>Current View</span></a>");
            output.WriteLine("<div class='verticalToolbarSeparator hiddenSmallView'></div>");
            output.WriteLine("<button id='secondaryToolbarToggle' class='toolbarButton' title='Tools' tabindex='36' data-l10n-id='tools'><span data-l10n-id='tools_label'>Tools</span></button>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='toolbarViewerMiddle'>");
            output.WriteLine("<div class='splitToolbarButton'>");
            output.WriteLine("<button id='zoomOut' class='toolbarButton zoomOut' title='Zoom Out' tabindex='21' data-l10n-id='zoom_out'><span data-l10n-id='zoom_out_label'>Zoom Out</span></button>");
            output.WriteLine("<div class='splitToolbarButtonSeparator'></div>");
            output.WriteLine("<button id='zoomIn' class='toolbarButton zoomIn' title='Zoom In' tabindex='22' data-l10n-id='zoom_in'><span data-l10n-id='zoom_in_label'>Zoom In</span></button>");
            output.WriteLine("</div>");
            output.WriteLine("<span id='scaleSelectContainer' class='dropdownToolbarButton'>");
            output.WriteLine("<select id='scaleSelect' title='Zoom' tabindex='23' data-l10n-id='zoom'>");
            output.WriteLine("<option id='pageAutoOption' title='' value='auto' selected='selected' data-l10n-id='page_scale_auto'>Automatic Zoom</option>");
            output.WriteLine("<option id='pageActualOption' title='' value='page-actual' data-l10n-id='page_scale_actual'>Actual Size</option>");
            output.WriteLine("<option id='pageFitOption' title='' value='page-fit' data-l10n-id='page_scale_fit'>Page Fit</option>");
            output.WriteLine("<option id='pageWidthOption' title='' value='page-width' data-l10n-id='page_scale_width'>Page Width</option>");
            output.WriteLine("<option id='customScaleOption' title='' value='custom' disabled='disabled' hidden='hidden'></option>");
            output.WriteLine("<option title='' value='0.5' data-l10n-id='page_scale_percent' data-l10n-args='{ \"scale\": 50 }'>50%</option>");
            output.WriteLine("<option title='' value='0.75' data-l10n-id='page_scale_percent' data-l10n-args='{ \"scale\": 75 }'>75%</option>");
            output.WriteLine("<option title='' value='1' data-l10n-id='page_scale_percent' data-l10n-args='{ \"scale\": 100 }'>100%</option>");
            output.WriteLine("<option title='' value='1.25' data-l10n-id='page_scale_percent' data-l10n-args='{ \"scale\": 125 }'>125%</option>");
            output.WriteLine("<option title='' value='1.5' data-l10n-id='page_scale_percent' data-l10n-args='{ \"scale\": 150 }'>150%</option>");
            output.WriteLine("<option title='' value='2' data-l10n-id='page_scale_percent' data-l10n-args='{ \"scale\": 200 }'>200%</option>");
            output.WriteLine("<option title='' value='3' data-l10n-id='page_scale_percent' data-l10n-args='{ \"scale\": 300 }'>300%</option>");
            output.WriteLine("<option title='' value='4' data-l10n-id='page_scale_percent' data-l10n-args='{ \"scale\": 400 }'>400%</option>");
            output.WriteLine("</select>");
            output.WriteLine("</span>");
            output.WriteLine("</div>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='loadingBar'>");
            output.WriteLine("<div class='progress'><div class='glimmer'></div></div>");
            output.WriteLine("</div>");

            output.WriteLine("</div>");
            output.WriteLine("</div>");
            #endregion

            #region viewerContextMenu
            output.WriteLine("<menu type='context' id='viewerContextMenu'>");
            output.WriteLine("<menuitem id='contextFirstPage' label='First Page' data-l10n-id='first_page'></menuitem>");
            output.WriteLine("<menuitem id='contextLastPage' label='Last Page' data-l10n-id='last_page'></menuitem>");
            output.WriteLine("<menuitem id='contextPageRotateCw' label='Rotate Clockwise' data-l10n-id='page_rotate_cw'></menuitem>");
            output.WriteLine("<menuitem id='contextPageRotateCcw' label='Rotate Counter-Clockwise' data-l10n-id='page_rotate_ccw'></menuitem>");
            output.WriteLine("</menu>");
            #endregion

            #region viewerContainer
            output.WriteLine("<div id='viewerContainer' tabindex='0'>");
            output.WriteLine("<div id='viewer' class='pdfViewer'></div>");
            output.WriteLine("</div>");

            #endregion

            #region errorWrapper
            output.WriteLine("<div id='errorWrapper' hidden='hidden'>");

            output.WriteLine("<div id='errorMessageLeft'>");
            output.WriteLine("<span id='errorMessage'></span>");
            output.WriteLine("<button id='errorShowMore' data-l10n-id='error_more_info'>More Information</button>");
            output.WriteLine("<button id='errorShowLess' data-l10n-id='error_less_info' hidden='hidden'>Less Information</button>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='errorMessageRight'>");
            output.WriteLine("<button id='errorClose' data-l10n-id='error_close'>Close</button>");
            output.WriteLine("</div>");

            output.WriteLine("<div class='clearBoth'></div>");
            output.WriteLine("<textarea id='errorMoreInfo' hidden='hidden' readonly='readonly'></textarea>");

            output.WriteLine("</div>");
            #endregion

            output.WriteLine("</div>");
            #endregion

            #region overlayContainer
            output.WriteLine("<div id='overlayContainer' class='hidden'>");

            output.WriteLine("<div id='passwordOverlay' class='container hidden'>");
            output.WriteLine("<div class='dialog'>");
            output.WriteLine("<div class='row'><p id='passwordText' data-l10n-id='password_label'>Enter the password to open this PDF file:</p></div>");
            output.WriteLine("<div class='row'><input type='password' id='password' class='toolbarField'></div>");
            output.WriteLine("<div class='buttonRow'>");
            output.WriteLine("<button id='passwordCancel' class='overlayButton'><span data-l10n-id='password_cancel'>Cancel</span></button>");
            output.WriteLine("<button id='passwordSubmit' class='overlayButton'><span data-l10n-id='password_ok'>OK</span></button>");
            output.WriteLine("</div>");
            output.WriteLine("</div>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='documentPropertiesOverlay' class='container hidden'>");
            output.WriteLine("<div class='dialog'>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_file_name'>File name:</span> <p id='fileNameField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_file_size'>File size:</span> <p id='fileSizeField'>-</p></div>");
            output.WriteLine("<div class='separator'></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_title'>Title:</span> <p id='titleField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_author'>Author:</span> <p id='authorField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_subject'>Subject:</span> <p id='subjectField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_keywords'>Keywords:</span> <p id='keywordsField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_creation_date'>Creation Date:</span> <p id='creationDateField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_modification_date'>Modification Date:</span> <p id='modificationDateField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_creator'>Creator:</span> <p id='creatorField'>-</p></div>");
            output.WriteLine("<div class='separator'></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_producer'>PDF Producer:</span> <p id='producerField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_version'>PDF Version:</span> <p id='versionField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_page_count'>Page Count:</span> <p id='pageCountField'>-</p></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_page_size'>Page Size:</span> <p id='pageSizeField'>-</p></div>");
            output.WriteLine("<div class='separator'></div>");
            output.WriteLine("<div class='row'><span data-l10n-id='document_properties_linearized'>Fast Web View:</span> <p id='linearizedField'>-</p></div>");
            output.WriteLine("<div class='buttonRow'><button id='documentPropertiesClose' class='overlayButton'><span data-l10n-id='document_properties_close'>Close</span></button></div>");
            output.WriteLine("</div>");
            output.WriteLine("</div>");

            output.WriteLine("<div id='printServiceOverlay' class='container hidden'>");
            output.WriteLine("<div class='dialog'>");
            output.WriteLine("<div class='row'><span data-l10n-id='print_progress_message'>Preparing document for printing…</span></div>");
            output.WriteLine("<div class='row'>");
            output.WriteLine("<progress value='0' max='100'></progress>");
            output.WriteLine("<span data-l10n-id='print_progress_percent' data-l10n-args='{ 'progress': 0 }' class='relative-progress'>0%</span>");
            output.WriteLine("</div>");
            output.WriteLine("<div class='buttonRow'><button id='printCancel' class='overlayButton'><span data-l10n-id='print_progress_close'>Cancel</span></button></div>");
            output.WriteLine("</div>");
            output.WriteLine("</div>");

            output.WriteLine("</div>");
            #endregion

            output.WriteLine("</div>");
            #endregion

            output.WriteLine("<div id='printContainer'></div>");

            output.WriteLine("</div>");
        }
    }
}
