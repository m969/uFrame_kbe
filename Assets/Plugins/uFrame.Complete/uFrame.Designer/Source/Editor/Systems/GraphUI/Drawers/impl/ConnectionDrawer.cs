using System.Collections.Generic;
using System.Linq;
using uFrame.Editor.GraphUI.ViewModels;
using uFrame.Editor.Platform;
using UnityEngine;

namespace uFrame.Editor.GraphUI.Drawers
{
    public class ConnectionDrawer : Drawer<ConnectionViewModel>
    {
        public override int ZOrder
        {
            get
            {
             
                
                return -1;
            }
        }

        public ConnectionDrawer(ConnectionViewModel viewModelObject) : base(viewModelObject)
        {
        }

        public override void Refresh(IPlatformDrawer platform, Vector2 position, bool hardRefresh = true)
        {
            base.Refresh(platform, position, hardRefresh);
            
        }

        public override void Draw(IPlatformDrawer platform, float scale)
        {
            base.Draw(platform, scale);
            var lines = ViewModel.DiagramViewModel.UseStraightLines;
            if (lines)
            {
                DrawStateLink(platform, scale);
            }
            else
            {
                DrawBeizureLink(platform, scale);
            }
        
        }

        private void DrawStateLink(IPlatformDrawer platform, float scale)
        {
            var _startPos = ViewModel.ConnectorB.Bounds.center;
            var _endPos = ViewModel.ConnectorA.Bounds.center;

            //var _startRight = ViewModel.ConnectorA.Direction == ConnectorDirection.Output;
            //var _endRight = ViewModel.ConnectorB.Direction == ConnectorDirection.Output;
            //Handles.color = ViewModel.CurrentColor;
            List<Vector2> points = new List<Vector2>();
            Vector2 curr;
            points.Add(curr = _startPos);
   
            if (_endPos.x < _startPos.x)
            {
                int offset = 0;
                if (_endPos.y < _startPos.y) offset = 10;
                points.Add(curr = curr + new Vector2(20f + offset, 0f));
                points.Add(curr = curr + new Vector2(0f, (_endPos.y - _startPos.y) / 2f + offset));
                points.Add(_endPos - new Vector2(20f + offset, (_endPos.y - _startPos.y) / 2f - offset));
                points.Add(_endPos - new Vector2(20f + offset, 0f));
            }
            else
            {
                points.Add(curr = _startPos + new Vector2((_endPos.x - _startPos.x)/ 2f,0f));
                points.Add(new Vector2(curr.x,_endPos.y));
            }
        
        
    
            points.Add(_endPos);
            var scaled = points.Select(p => new Vector2(p.x * scale,p.y * scale)).ToArray();

            platform.DrawPolyLine(scaled, ViewModel.CurrentColor);
            platform.DrawPolyLine(scaled.Select(p => p + new Vector2(1f, 1f)).ToArray(), ViewModel.CurrentColor);
        
        
        }

        private void DrawBeizureLink(IPlatformDrawer platform, float scale)
        {
            var _startPos = ViewModel.ConnectorA.Bounds.center;
            var _endPos = ViewModel.ConnectorB.Bounds.center;

            var _startRight = ViewModel.ConnectorA.Direction == ConnectorDirection.Output;
            var _endRight = ViewModel.ConnectorB.Direction == ConnectorDirection.Output;


            var multiplier = Mathf.Min(30f, (_endPos.x - _startPos.x)*0.3f);


            var m2 = 3;
            if (multiplier < 0)
            {
                _startRight = !_startRight;
                _endRight = !_endRight;
            }


            var startTan = _startPos + (_endRight ? -Vector2.right*m2 : Vector2.right*m2)*multiplier;

            var endTan = _endPos + (_startRight ? -Vector2.right*m2 : Vector2.right*m2)*multiplier;

            var shadowCol = new Color(0, 0, 0, 0.1f);
            
            //if (ViewModel.IsFullColor)
                for (int i = 0; i < 3; i++) // Draw a shadow
                    platform.DrawBezier(_startPos * scale, _endPos * scale, startTan * scale,
                        endTan*scale, shadowCol,  (i + 1)*5);

            platform.DrawBezier(_startPos * scale, _endPos * scale, startTan * scale,
                endTan*scale, ViewModel.CurrentColor,  3);
        }
    }
}