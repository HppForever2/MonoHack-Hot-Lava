namespace HotLava_Cheat.Source.Visuals.Render
{
    public class Game
    {
        private static UnityEngine.GUIStyle textStyle;
        private static UnityEngine.GUIStyle shadowStyle;

        private static bool stylesInitialized = false;

        private static readonly System.Reflection.FieldInfo fiInfoCamera = HarmonyLib.AccessTools.Field(typeof(Klei.HotLava.Game.Info), "Camera");
        private static readonly System.Reflection.MethodInfo miInfoWorldToScreenPoint = HarmonyLib.AccessTools.Method(typeof(Klei.HotLava.Game.Info), "WorldToScreenPoint", new System.Type[] { typeof(UnityEngine.Vector3) });

        public static void Initialize()
        {
            MarkerRenderer.Initialize();
        }

        public static void Render()
        {
            if (!ShouldRenderOverlay())
                return;

            RenderRecordingInfo();
            RenderCenterNotification();
            RenderStartMarkerText();
        }

        private static bool ShouldRenderOverlay()
        {
            return !NS_Visuals.GUIManager.bShowGUI && NS_Core.Vars.sGame.bIn && NS_Core.Movement.Record.HasOverlayState;
        }

        private static bool ShouldRenderMarker()
        {
            return NS_Core.Movement.Record.TotalFrames > 0;
        }

        private static void InitStyles()
        {
            if (stylesInitialized)
                return;

            textStyle = new UnityEngine.GUIStyle();
            textStyle.fontSize = 18;
            textStyle.fontStyle = UnityEngine.FontStyle.Bold;
            textStyle.normal.textColor = UnityEngine.Color.white;
            textStyle.alignment = UnityEngine.TextAnchor.UpperRight;
            textStyle.richText = false;

            shadowStyle = new UnityEngine.GUIStyle(textStyle);
            shadowStyle.normal.textColor = UnityEngine.Color.black;

            stylesInitialized = true;
        }

        private static void RenderRecordingInfo()
        {
            if (!NS_Core.Movement.Record.IsPreparingRecord && !NS_Core.Movement.Record.IsRecording && !NS_Core.Movement.Record.IsPlaying && !NS_Core.Movement.Record.IsAutoApproaching && !NS_Core.Movement.Record.IsRewinding && NS_Core.Movement.Record.StagedFramesCount <= 0)
                return;

            InitStyles();

            float xPos = UnityEngine.Screen.width - 20f;
            float yPos = 20f;

            textStyle.alignment = UnityEngine.TextAnchor.UpperRight;
            shadowStyle.alignment = UnityEngine.TextAnchor.UpperRight;

            string pathText = GetOverlayPathText();
            string sectionText = NS_Core.Movement.Record.CurrentSectionDisplay;

            if (!string.IsNullOrWhiteSpace(pathText))
            {
                textStyle.normal.textColor = new UnityEngine.Color(0.9f, 0.95f, 1f);
                yPos = RenderShadowLabel(pathText, xPos, yPos);
            }

            if (!string.IsNullOrWhiteSpace(sectionText))
            {
                textStyle.normal.textColor = new UnityEngine.Color(0.78f, 0.88f, 1f);
                yPos = RenderShadowLabel(sectionText, xPos, yPos);
            }

            string statusText = string.Empty;
            string segmentText = GetSegmentStatusText();

            if (NS_Core.Movement.Record.IsPreparingRecord)
            {
                statusText = $"REC DELAY: {NS_Core.Movement.Record.RecordDelayRemainingFrames}/{NS_Core.Movement.Record.RecordDelayTotalFrames} FRAMES | SLOWMO: {NS_Core.Movement.Record.RecordSlowmotionScale:F2}x{segmentText}";
                textStyle.normal.textColor = new UnityEngine.Color(1f, 0.74f, 0.18f);
            }

            else if (NS_Core.Movement.Record.IsRecording)
            {
                statusText = $"REC: {NS_Core.Movement.Record.StagedFramesCount} FRAMES | MAIN: {NS_Core.Movement.Record.TotalFrames} FRAMES | SLOWMO: {NS_Core.Movement.Record.RecordSlowmotionScale:F2}x{segmentText}";
                textStyle.normal.textColor = UnityEngine.Color.red;
            }

            else if (NS_Core.Movement.Record.IsRewinding)
            {
                statusText = $"REWIND: {NS_Core.Movement.Record.CurrentFrame}/{NS_Core.Movement.Record.RewindFramesCount} FRAMES | SPEED: {NS_Core.Movement.Record.CurrentRewindSpeed:F2}x{segmentText}";
                textStyle.normal.textColor = new UnityEngine.Color(0.44f, 0.9f, 1f);
            }

            else if (NS_Core.Movement.Record.IsAutoApproaching)
            {
                statusText = "AUTO-APPROACH";
                textStyle.normal.textColor = UnityEngine.Color.yellow;
            }

            else if (NS_Core.Movement.Record.IsRangePlayback)
            {
                statusText = $"REPLAY: {NS_Core.Movement.Record.CurrentFrame}/{NS_Core.Movement.Record.PlaybackTargetFrame + 1} FRAMES | DESYNC: {NS_Core.Movement.Record.CurrentDesync:F3}m{segmentText}";
                textStyle.normal.textColor = new UnityEngine.Color(0.68f, 0.92f, 1f);
            }

            else if (NS_Core.Movement.Record.IsPlaying)
            {
                statusText = $"PLAY: {NS_Core.Movement.Record.CurrentFrame}/{NS_Core.Movement.Record.TotalFrames} FRAMES | DESYNC: {NS_Core.Movement.Record.CurrentDesync:F3}m{segmentText}";
                textStyle.normal.textColor = GetDesyncColor(NS_Core.Movement.Record.CurrentDesync);
            }

            else if (NS_Core.Movement.Record.StagedFramesCount > 0)
            {
                statusText = $"BUFFER: {NS_Core.Movement.Record.StagedFramesCount} FRAMES | APPLY READY{segmentText}";
                textStyle.normal.textColor = new UnityEngine.Color(0.48f, 1f, 0.68f);
            }

            if (!string.IsNullOrWhiteSpace(statusText))
                RenderShadowLabel(statusText, xPos, yPos);
        }

        private static void RenderCenterNotification()
        {
            if (!NS_Core.Movement.Record.HasCenterNotification)
                return;

            InitStyles();

            textStyle.alignment = UnityEngine.TextAnchor.MiddleCenter;
            shadowStyle.alignment = UnityEngine.TextAnchor.MiddleCenter;
            textStyle.normal.textColor = new UnityEngine.Color(0.92f, 0.98f, 1f);

            string text = NS_Core.Movement.Record.CenterNotificationText;

            UnityEngine.Vector2 size = textStyle.CalcSize(new UnityEngine.GUIContent(text));

            float x = UnityEngine.Screen.width * 0.5f - size.x * 0.5f;
            float y = UnityEngine.Screen.height * 0.5f - 56f;

            UnityEngine.Rect rect = new UnityEngine.Rect(x, y, size.x, size.y);
            UnityEngine.Rect shadowRect = new UnityEngine.Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height);

            UnityEngine.GUI.Label(shadowRect, text, shadowStyle);
            UnityEngine.GUI.Label(rect, text, textStyle);
        }

        private static string GetOverlayPathText()
        {
            string recordedPathText = NS_Core.Movement.Record.RecordedPathFileDisplay;

            if (!string.IsNullOrWhiteSpace(recordedPathText) && (
                NS_Core.Movement.Record.TotalFrames > 0 ||
                NS_Core.Movement.Record.StagedFramesCount > 0 ||
                NS_Core.Movement.Record.IsPreparingRecord ||
                NS_Core.Movement.Record.IsRecording ||
                NS_Core.Movement.Record.IsRewinding ||
                NS_Core.Movement.Record.IsPlaying ||
                NS_Core.Movement.Record.IsAutoApproaching ||
                NS_Core.Movement.Record.IsRangePlayback))
            {
                return recordedPathText;
            }

            return NS_Core.Movement.Record.CurrentPathDisplay;
        }

        private static string GetSegmentStatusText()
        {
            if (NS_Core.Movement.Record.TotalSegments <= 0 || NS_Core.Movement.Record.CurrentSegment <= 0)
                return string.Empty;

            return $" | SEG: {NS_Core.Movement.Record.CurrentSegment}/{NS_Core.Movement.Record.TotalSegments}";
        }

        private static float RenderShadowLabel(string text, float xPos, float yPos)
        {
            UnityEngine.Vector2 size = textStyle.CalcSize(new UnityEngine.GUIContent(text));
            UnityEngine.Rect rect = new UnityEngine.Rect(xPos - size.x, yPos, size.x, size.y);
            UnityEngine.Rect shadowRect = new UnityEngine.Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height);

            UnityEngine.GUI.Label(shadowRect, text, shadowStyle);
            UnityEngine.GUI.Label(rect, text, textStyle);

            return yPos + size.y + 2f;
        }

        private static void RenderStartMarkerText()
        {
            if (!TryGetMarkerPosition(out Klei.HotLava.Character.PlayerController player, out UnityEngine.Vector3 startPos))
                return;

            UnityEngine.Camera camera = GetGameCamera();

            if (camera == null)
                return;

            UnityEngine.Vector3 playerPos = player.RigidBody.position;

            float distance = UnityEngine.Vector2.Distance(new UnityEngine.Vector2(playerPos.x, playerPos.z), new UnityEngine.Vector2(startPos.x, startPos.z));

            UnityEngine.Vector3 worldTextPos = startPos + UnityEngine.Vector3.up * 0.15f;

            if (!TryGetMarkerScreenPosition(worldTextPos, camera, out UnityEngine.Vector3 screenPos))
                return;

            screenPos.y = UnityEngine.Screen.height - screenPos.y;

            InitStyles();

            string recordNameText = NS_Core.Movement.Record.RecordedPathFileDisplay;

            if (!string.IsNullOrWhiteSpace(recordNameText))
                RenderMarkerCenteredLabel(recordNameText, screenPos, -18f, new UnityEngine.Color(0.9f, 0.95f, 1f), 12, UnityEngine.FontStyle.Bold);

            RenderMarkerCenteredLabel($"{distance:F1}m", screenPos, 2f, UnityEngine.Color.cyan, 18, UnityEngine.FontStyle.Bold);
        }

        private static bool TryGetMarkerScreenPosition(UnityEngine.Vector3 worldTextPos, UnityEngine.Camera camera, out UnityEngine.Vector3 screenPos)
        {
            screenPos = UnityEngine.Vector3.zero;

            if (camera == null)
                return false;

            UnityEngine.Vector3 viewportPos = camera.WorldToViewportPoint(worldTextPos);

            if (viewportPos.z <= 0.05f || !IsFiniteFloat(viewportPos.x) || !IsFiniteFloat(viewportPos.y) || viewportPos.x < 0f || viewportPos.x > 1f || viewportPos.y < 0f || viewportPos.y > 1f)
                return false;

            try
            {
                screenPos = miInfoWorldToScreenPoint != null ? (UnityEngine.Vector3)miInfoWorldToScreenPoint.Invoke(null, new object[] { worldTextPos }) : camera.WorldToScreenPoint(worldTextPos);
            }

            catch
            {
                screenPos = camera.WorldToScreenPoint(worldTextPos);
            }

            if (!IsFiniteFloat(screenPos.x) || !IsFiniteFloat(screenPos.y) || !IsFiniteFloat(screenPos.z) || screenPos.z <= 0.05f)
                return false;

            if (screenPos.x < 0f || screenPos.x > UnityEngine.Screen.width || screenPos.y < 0f || screenPos.y > UnityEngine.Screen.height)
                return false;

            return true;
        }

        private static bool IsFiniteFloat(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value);
        }

        private static void RenderMarkerCenteredLabel(string text, UnityEngine.Vector3 screenPos, float yOffset, UnityEngine.Color color, int fontSize, UnityEngine.FontStyle fontStyle)
        {
            UnityEngine.GUIStyle mainStyle = new UnityEngine.GUIStyle(textStyle);

            mainStyle.alignment = UnityEngine.TextAnchor.MiddleCenter;
            mainStyle.fontSize = fontSize;
            mainStyle.fontStyle = fontStyle;
            mainStyle.normal.textColor = color;

            UnityEngine.GUIStyle markerShadowStyle = new UnityEngine.GUIStyle(mainStyle);

            markerShadowStyle.normal.textColor = UnityEngine.Color.black;

            UnityEngine.Vector2 size = mainStyle.CalcSize(new UnityEngine.GUIContent(text));
            UnityEngine.Rect rect = new UnityEngine.Rect(screenPos.x - size.x / 2f, screenPos.y + yOffset - size.y / 2f, size.x, size.y);
            UnityEngine.Rect shadowRect = new UnityEngine.Rect(rect.x + 1f, rect.y + 1f, rect.width, rect.height);

            UnityEngine.GUI.Label(shadowRect, text, markerShadowStyle);
            UnityEngine.GUI.Label(rect, text, mainStyle);
        }

        private static UnityEngine.Camera GetGameCamera()
        {
            UnityEngine.Camera infoCamera = fiInfoCamera != null ? fiInfoCamera.GetValue(null) as UnityEngine.Camera : null;
            return infoCamera != null ? infoCamera : UnityEngine.Camera.main;
        }

        private static bool TryGetMarkerPosition(out Klei.HotLava.Character.PlayerController player, out UnityEngine.Vector3 startPos)
        {
            player = NS_Core.Binds.GetLocalPlayer();
            startPos = UnityEngine.Vector3.zero;

            if (!ShouldRenderMarker())
                return false;

            if (player == null || !player.IsMine)
                return false;

            if (!NS_Core.Movement.Record.IsCurrentPathMatchingRecording())
                return false;

            startPos = GetGroundedMarkerPosition(NS_Core.Movement.Record.RecordStartPosition);
            return true;
        }

        private static UnityEngine.Vector3 GetGroundedMarkerPosition(UnityEngine.Vector3 position)
        {
            UnityEngine.Vector3 rayStart = position + UnityEngine.Vector3.up * 3f;
            UnityEngine.RaycastHit[] hits = UnityEngine.Physics.RaycastAll(rayStart, UnityEngine.Vector3.down, 64f, ~0, UnityEngine.QueryTriggerInteraction.Ignore);

            if (hits != null && hits.Length > 0)
            {
                System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

                for (int i = 0; i < hits.Length; i++)
                {
                    if (IsPreferredMarkerGroundHit(hits[i]))
                        return hits[i].point + UnityEngine.Vector3.up * 0.03f;
                }

                for (int i = 0; i < hits.Length; i++)
                {
                    if (IsFallbackMarkerGroundHit(hits[i]))
                        return hits[i].point + UnityEngine.Vector3.up * 0.03f;
                }
            }

            return position;
        }

        private static bool IsPreferredMarkerGroundHit(UnityEngine.RaycastHit hit)
        {
            if (!IsFallbackMarkerGroundHit(hit))
                return false;

            return hit.rigidbody == null;
        }

        private static bool IsFallbackMarkerGroundHit(UnityEngine.RaycastHit hit)
        {
            if (hit.collider == null || hit.collider.isTrigger)
                return false;

            if (hit.collider.GetComponentInParent<Klei.HotLava.Character.PlayerController>() != null)
                return false;

            return true;
        }

        private static UnityEngine.Color GetDesyncColor(float desync)
        {
            UnityEngine.Color green = new UnityEngine.Color(0.18f, 0.95f, 0.35f);
            UnityEngine.Color orange = new UnityEngine.Color(1f, 0.58f, 0.12f);
            UnityEngine.Color yellow = new UnityEngine.Color(1f, 0.87f, 0.22f);
            UnityEngine.Color red = new UnityEngine.Color(1f, 0.28f, 0.28f);

            if (desync <= 0.3f)
                return green;

            if (desync < 0.5f)
                return UnityEngine.Color.Lerp(green, orange, UnityEngine.Mathf.SmoothStep(0f, 1f, UnityEngine.Mathf.InverseLerp(0.3f, 0.5f, desync)));

            if (desync < 1f)
                return UnityEngine.Color.Lerp(orange, yellow, UnityEngine.Mathf.SmoothStep(0f, 1f, UnityEngine.Mathf.InverseLerp(0.5f, 1f, desync)));

            return UnityEngine.Color.Lerp(yellow, red, UnityEngine.Mathf.SmoothStep(0f, 1f, UnityEngine.Mathf.Clamp01((desync - 1f) / 1.25f)));
        }

        private class MarkerRenderer : UnityEngine.MonoBehaviour
        {
            private static MarkerRenderer instance;
            private UnityEngine.Material lineMaterial;

            public static void Initialize()
            {
                if (instance != null)
                    return;

                UnityEngine.GameObject go = new UnityEngine.GameObject("MRMarkerRenderer");
                UnityEngine.Object.DontDestroyOnLoad(go);

                instance = go.AddComponent<MarkerRenderer>();
            }

            private void Awake()
            {
                UnityEngine.Shader shader = UnityEngine.Shader.Find("Hidden/Internal-Colored");

                lineMaterial = new UnityEngine.Material(shader);
                lineMaterial.hideFlags = UnityEngine.HideFlags.HideAndDontSave;
                lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                lineMaterial.SetInt("_ZWrite", 0);
            }

            private void OnRenderObject()
            {
                if (!ShouldRenderOverlay() || !ShouldRenderMarker())
                    return;

                if (!TryGetMarkerPosition(out Klei.HotLava.Character.PlayerController player, out UnityEngine.Vector3 startPos))
                    return;

                if (lineMaterial == null)
                    return;

                lineMaterial.SetPass(0);

                UnityEngine.GL.PushMatrix();
                UnityEngine.GL.MultMatrix(UnityEngine.Matrix4x4.identity);
                UnityEngine.GL.Begin(UnityEngine.GL.LINES);
                UnityEngine.GL.Color(new UnityEngine.Color(0f, 1f, 1f, 0.8f));
                UnityEngine.GL.Vertex3(startPos.x, startPos.y, startPos.z);
                UnityEngine.GL.Vertex3(startPos.x, startPos.y + 5f, startPos.z);
                UnityEngine.GL.End();

                UnityEngine.GL.Begin(UnityEngine.GL.LINES);

                int segments = 16;
                float radius = 0.5f;

                for (int i = 0; i < segments; i++)
                {
                    float angle1 = (float)i / segments * UnityEngine.Mathf.PI * 2f;
                    float angle2 = (float)(i + 1) / segments * UnityEngine.Mathf.PI * 2f;

                    UnityEngine.Vector3 p1 = startPos + new UnityEngine.Vector3(UnityEngine.Mathf.Cos(angle1) * radius, 0.01f, UnityEngine.Mathf.Sin(angle1) * radius);
                    UnityEngine.Vector3 p2 = startPos + new UnityEngine.Vector3(UnityEngine.Mathf.Cos(angle2) * radius, 0.01f, UnityEngine.Mathf.Sin(angle2) * radius);

                    UnityEngine.GL.Color(new UnityEngine.Color(0f, 1f, 1f, 0.6f));
                    UnityEngine.GL.Vertex3(p1.x, p1.y, p1.z);
                    UnityEngine.GL.Vertex3(p2.x, p2.y, p2.z);
                }

                UnityEngine.GL.End();
                UnityEngine.GL.PopMatrix();
            }
        }
    }
}